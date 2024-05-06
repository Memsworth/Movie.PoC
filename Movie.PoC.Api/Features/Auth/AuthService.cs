using FluentValidation;
using LanguageExt.Common;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;

namespace Movie.PoC.Api.Features.Auth;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<UserRegisterRequest> _validator;

    public AuthService(ApplicationDbContext dbContext, IValidator<UserRegisterRequest> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    public async Task<Result<Guid>> RegisterUser(UserRegisterRequest userRegistrationData)
    {
        var validationResult = await _validator.ValidateAsync(userRegistrationData);
        if (!validationResult.IsValid) 
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Guid>(error);
        }

        var newUser = userRegistrationData.MapToUserModel();
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return newUser.Id;
    }
}