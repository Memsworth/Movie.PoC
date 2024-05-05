using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;

namespace Movie.PoC.Api.Features.Auth;

public class RegisterUserCommandValidator : AbstractValidator<UserRegisterRequest>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty().WithMessage("First name can't be empty.");
        RuleFor(request => request.LastName).NotEmpty().WithMessage("Last name can't be empty.");
        RuleFor(request => request.Birthday.Year).NotEmpty()
            .WithMessage("Birthday can't be empty")
            .LessThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage("Below the allowable age");

        RuleFor(request => request.Email).NotEmpty().WithMessage("Email can't be empty")
            .EmailAddress().WithMessage("Enter a valid email address");

        //TODO: ADD IN A MATCHING LATER
        RuleFor(request => request.Password).NotEmpty().WithMessage("Password can't be empty")
            .MinimumLength(8).WithMessage("Password needs to be at-least 8 characters");
    }
}

public class RegisterUserCommandBusinessValidator : AbstractValidator<UserRegisterRequest>
{
    private readonly ApplicationDbContext _dbContext;
    public RegisterUserCommandBusinessValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Email).Must(EmailBeUnique).WithMessage("Email exists. Please enter another email");
    }

    private bool EmailBeUnique(string email)
    {
        var item = _dbContext.Users.AnyAsync(x => x.Email == email).Result;
        return !item;
    }
    
}