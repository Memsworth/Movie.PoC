using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Database;
using BC = BCrypt.Net.BCrypt;


namespace Movie.PoC.Api.Features.Auth;

public record LoginQuery(LoginRequest UserLoginRequest) : IRequest<Result<string>>;

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
        var inputValidation = await _validator.ValidateAsync(request.UserLoginRequest,
            cancellationToken);
        
        if (!inputValidation.IsValid)
        {
            return Helper.GetValidation<string>(inputValidation);
        }

        var businessValidation = new ValidationResult();
        
        //Check if user is there
        var user = await _dbContext.Users.FirstOrDefaultAsync
        (x => x.Email == request.UserLoginRequest.Email,
            cancellationToken);
        
        // check if user is there
        if (user is null)
        {
            businessValidation.Errors.Add(new ValidationFailure("User", "User not found"));
        }
        if (!BC.Verify(request.UserLoginRequest.Password, user.Password))
        {
            businessValidation.Errors.Add(new ValidationFailure("Email", "Invalid Login Details"));
        }

        if (!businessValidation.IsValid)
        {
            return Helper.GetValidation<string>(businessValidation);
        }

        //Get an IMAPPER interface and map 
        var userTokenData = user.MapToUserToken();

        var userToken = _tokenGeneratorService.GenerateToken(userTokenData);
        return userToken;
    }
}