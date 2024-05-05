using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using BC = BCrypt.Net.BCrypt;


namespace Movie.PoC.Api.Features.Auth;

public record LoginQuery(LoginRequest LoginData) : IRequest<Result<string>>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<string>>
{
    private readonly IValidator<LoginRequest> _validator;
    private readonly ITokenService _tokenGeneratorService;
    private readonly ApplicationDbContext _dbContext;

    public LoginQueryHandler(IValidator<LoginRequest> validator, ITokenService tokenGeneratorService,
        ApplicationDbContext dbContext)
    {
        _validator = validator;
        _tokenGeneratorService = tokenGeneratorService;
        _dbContext = dbContext;
    }
    
    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var businessValidation = new ValidationResult();
        
        //Check if user is there
        var user = await _dbContext.Users.FirstOrDefaultAsync
        (x => x.Email == request.LoginData.Email,
            cancellationToken);
        
        // check if user is there
        if (user is null)
        {
            businessValidation.Errors.Add(new ValidationFailure("User", "User doesn't exist"));
        }
        if (!BC.Verify(request.LoginData.Password, user.Password))
        {
            businessValidation.Errors.Add(new ValidationFailure("Email", "Invalid Login Details"));
        }

        if (!businessValidation.IsValid)
        {
            var error = new ValidationException(businessValidation.Errors);
            return new Result<string>(error);
        }

        //Mapping userData to a token claim
        var userTokenData = user.MapToUserToken();
        var userToken = _tokenGeneratorService.GenerateToken(userTokenData);
        return userToken;
    }
}