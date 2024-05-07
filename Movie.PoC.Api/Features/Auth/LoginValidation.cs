using FluentValidation;
using Movie.PoC.Api.Contracts.Requests;

namespace Movie.PoC.Api.Features.Auth;

public class LoginValidation : AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(userInput => userInput.Email)
            .EmailAddress().WithMessage("Enter a valid email");
        RuleFor(userInput => userInput.Password)
            .NotEmpty().WithMessage("Password can't be empty");
    }
}