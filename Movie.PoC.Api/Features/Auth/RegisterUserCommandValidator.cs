using FluentValidation;
using Movie.PoC.Api.Contracts.Requests;

namespace Movie.PoC.Api.Features.Auth;

public class RegisterUserCommandValidator : AbstractValidator<CreateUserRequest>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name can't be empty.");
        RuleFor(request => request.BirthDay).NotEmpty()
            .WithMessage("Birthday can't be empty")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Birthday can't be today's date");

        RuleFor(request => request.Email).NotEmpty().WithMessage("Email can't be empty")
            .EmailAddress().WithMessage("Enter a valid email address");

        //TODO: ADD IN A MATCHING LATER
        RuleFor(request => request.Password).NotEmpty().WithMessage("Password can't be empty")
            .MinimumLength(8).WithMessage("Password needs to be at-least 8 characters");
    }
}