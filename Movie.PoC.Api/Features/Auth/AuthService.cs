using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Models.Contracts;
using Movie.PoC.Api.Requests;
using BC = BCrypt.Net.BCrypt;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Movie.PoC.Api.Features.Auth;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<UserRegisterRequest> _userValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext dbContext, IValidator<UserRegisterRequest> userValidator,
        ITokenService tokenService, IValidator<LoginRequest> loginValidator)
    {
        _dbContext = dbContext;
        _userValidator = userValidator;
        _tokenService = tokenService;
        _loginValidator = loginValidator;
    }
    public async Task<Result<Guid>> RegisterUser(UserRegisterRequest userRegistrationData)
    {
        var validationResult = await _userValidator.ValidateAsync(userRegistrationData);
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

    public async Task<Result<string?>> LoginUser(LoginRequest requestData)
    {
        //applying validation here
        var inputValidationResult = await _loginValidator.ValidateAsync(requestData);

        if (!inputValidationResult.IsValid)
        {
            var errors = new ValidationException(inputValidationResult.Errors);
            return new Result<string?>(errors);
        }
        var user = await _dbContext.Users.FirstOrDefaultAsync
        (x => x.Email == requestData.Email);

        if (user is null)
        {
            return null;
        }
        
        if (!BC.Verify(requestData.Password, user.Password))
        {
            var result = new ValidationResult();
            result.Errors.Add(new ValidationFailure("Password", "Enter the correct password"));
            var error = new ValidationException(result.Errors);
            return new Result<string?>(error);
        }
        
        var userTokenData = user.MapToUserToken();
        var userToken = _tokenService.GenerateToken(userTokenData);
        return userToken;
    }
}