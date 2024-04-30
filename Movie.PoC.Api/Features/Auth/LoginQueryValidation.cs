/*using FluentValidation;
using Movie.PoC.Api.Contracts.Requests;

namespace Movie.PoC.Api.Features.Auth;

public class LoginQueryValidation : AbstractValidator<LoginRequest>
{
    public LoginQueryValidation()
    {
        RuleFor(userInput => userInput.Email)
            .EmailAddress().WithMessage("Enter a valid email")
            .NotEmpty().WithMessage("Email can't be empty");

        RuleFor(userInput => userInput.Password)
            .NotEmpty().WithMessage("Password can't be empty");
    }
}*/