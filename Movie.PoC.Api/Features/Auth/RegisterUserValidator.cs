using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;

namespace Movie.PoC.Api.Features.Auth;

public class RegisterUserValidator : AbstractValidator<UserRegisterRequest>
{
    private readonly ApplicationDbContext _dbContext;
    public RegisterUserValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(request => request.FirstName).NotEmpty().WithMessage("First name can't be empty.");
        RuleFor(request => request.LastName).NotEmpty().WithMessage("Last name can't be empty.");
        RuleFor(request => request.Birthday)
            .NotEmpty().WithMessage("Birthday can't be empty")
            .Must(BeAValidAge).WithMessage("You must be at least 18 years old.");

        RuleFor(request => request.Email)
            .EmailAddress().WithMessage("Enter a valid email address");

        //TODO: ADD IN A MATCHING LATER
        RuleFor(request => request.Password).NotEmpty().WithMessage("Password can't be empty")
            .MinimumLength(8).WithMessage("Password needs to be at-least 8 characters");

        RuleFor(x => x.Email).Must(EmailBeUnique).WithMessage("Email exists. Please enter another email");
        
    }
    
    private bool BeAValidAge(DateTime dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
        return age >= 18;
    }
    
    private bool EmailBeUnique(string email)
    {
        var item = _dbContext.Users.AnyAsync(x => x.Email == email).Result;
        return !item;
    }
}