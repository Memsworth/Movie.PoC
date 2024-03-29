﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.DTOs;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using SimpleResults;
using BC = BCrypt.Net.BCrypt;


namespace Movie.PoC.Api.Features.Auth;

public record LoginQuery(LoginRequest UserLoginRequest) : IRequest<Result<string>>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<string>>
{
    private IValidator<LoginRequest> _validator;
    private ITokenService _tokenGeneratorService;
    private ApplicationDbContext _dbContext;

    public LoginQueryHandler(IValidator<LoginRequest> validator, ITokenService tokenGeneratorService,
        ApplicationDbContext dbContext)
    {
        _validator = validator;
        _tokenGeneratorService = tokenGeneratorService;
        _dbContext = dbContext;
    }
    
    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
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

        // check if login detail is correct
        if (!BC.Verify(request.UserLoginRequest.Password, user.Password))
        {
            validationResults.Errors.Add(new ValidationFailure(
                nameof(LoginQuery.UserLoginRequest.Password), "invalid password"));
            return Result.Invalid(validationResults.AsErrors());
        }

        //Get an IMAPPER interface and map 
        var userDataToken = new UserTokenDto()
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            Name = user.Name,
            Role = user.Role.ToString(),

        };

        var userToken = _tokenGeneratorService.GenerateToken(userDataToken);
        return Result.Success().ToResult(userToken);
    }
}