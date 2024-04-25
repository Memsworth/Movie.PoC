using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;
using SimpleResults;
using BC = BCrypt.Net.BCrypt;
namespace Movie.PoC.Api.Features.Auth;

public record RegisterUserCommand(CreateUserRequest UserInput) : IRequest<Result<CreatedGuid>>;


public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<CreatedGuid>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<CreateUserRequest> _validator;

    public RegisterUserCommandHandler(ApplicationDbContext dbContext, 
        IValidator<CreateUserRequest> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Result<CreatedGuid>> Handle(RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = await _validator.ValidateAsync(request.UserInput, cancellationToken);
        if (inputValidationResult.IsFailed())
        {
            return Result.Invalid(inputValidationResult.AsErrors());
        }

        var businessValidationResult = new ValidationResult();

        if (await IsEmailUnique(request.UserInput.Email))
        {
            businessValidationResult.Errors.Add(new ValidationFailure("Email",
                "Email already exists"));
            return Result.Invalid(businessValidationResult.AsErrors());
        }
        
        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            Name = request.UserInput.Name,
            Email = request.UserInput.Email,
            Password = BC.HashPassword(request.UserInput.Password),
            BirthDay = request.UserInput.BirthDay,
            Role = Role.User
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.CreatedResource(user.Id);
    }
    private async Task<bool> IsEmailUnique(string email) => await _dbContext.Users.AnyAsync(x => x.Email == email);
} 