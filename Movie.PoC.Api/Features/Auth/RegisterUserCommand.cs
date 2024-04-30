using FluentValidation;
using LanguageExt.Common;
using MediatR;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
namespace Movie.PoC.Api.Features.Auth;

public record RegisterUserCommand(UserRegisterRequest RequestData) : IRequest<Result<Guid>>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<UserRegisterRequest> _validator;

    public RegisterUserCommandHandler(ApplicationDbContext dbContext, 
        IValidator<UserRegisterRequest> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.RequestData, cancellationToken);
        if (!validationResult.IsValid)
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Guid>(error);
        }
        
        var newUser = request.RequestData.MapToUserModel();
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return newUser.Id;
    }

} 