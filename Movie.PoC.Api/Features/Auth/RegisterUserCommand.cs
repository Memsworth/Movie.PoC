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

public record RegisterUserCommand(CreateUserRequest UserInput) : IRequest<Result>;


public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<CreateUserRequest> _validator;

    public RegisterUserCommandHandler(ApplicationDbContext dbContext, 
        IValidator<CreateUserRequest> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Result> Handle(RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(request.UserInput, cancellationToken);
        if (validationResults.IsFailed())
        {
            return Result.Invalid(validationResults.AsErrors());
        }
        
        if (await IsEmailUnique(request.UserInput.Email))
        {
            validationResults.Errors.Add(new ValidationFailure(nameof(request.UserInput.Email), "Email already exists"));
            return Result.Invalid(validationResults.AsErrors());
        }
        
        var user = new UserModel
        {
            Name = request.UserInput.Name,
            Email = request.UserInput.Email,
            Password = BC.HashPassword(request.UserInput.Password),
            BirthDay = request.UserInput.BirthDay,
            Role = Role.User
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.CreatedResource();
    }
    private async Task<bool> IsEmailUnique(string email) => await _dbContext.Users.AnyAsync(x => x.Email == email);
} 