using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using SimpleResults;

namespace Movie.PoC.Api.Features.Auth;

public record LoginQuery(LoginRequest UserLoginRequest) : IRequest<Result>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result>
{
    private IValidator<LoginRequest> _validator;
    private ITokenGeneratorService _tokenGeneratorService;
    private ApplicationDbContext _dbContext;

    public LoginQueryHandler(IValidator<LoginRequest> validator, ITokenGeneratorService tokenGeneratorService,
        ApplicationDbContext dbContext)
    {
        _validator = validator;
        _tokenGeneratorService = tokenGeneratorService;
        _dbContext = dbContext;
    }
    
    public async Task<Result> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(request.UserLoginRequest,
            cancellationToken);
        
        if (validationResults.IsFailed())
        {
            return Result.Invalid(validationResults.AsErrors());
        }
        //Check if user is there
        var user = await _dbContext.Users.FirstOrDefaultAsync
        (x => x.Email == request.UserLoginRequest.Email,
            cancellationToken);
        
        if (user is null)
        {
            validationResults.Errors.Add(new ValidationFailure
                (nameof(LoginQuery.UserLoginRequest.Email), "user doesn't exist"));
            return Result.NotFound(validationResults.AsErrors());
        }
        
        
    }
}