using Domain.Abstractions;
using Domain.Dtos;
using FluentValidation;

namespace Services.ModelValidation
{
    public class RegistrationValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationValidator()
        {
            RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid Email Address");

            RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("Password is required");

            RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Matches("^[a-zA-Z ]+$")
            .WithMessage("Name should only contain letters and spaces");


            RuleFor(dto => dto.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of Birth is required");
        }
    }
}
