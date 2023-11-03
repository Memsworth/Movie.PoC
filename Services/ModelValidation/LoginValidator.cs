using Domain.Dtos;
using FluentValidation;

namespace Services.ModelValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid Email Address");

            RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("Password is required");
        }
    }
}
