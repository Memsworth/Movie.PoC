using Domain.Abstractions;
using Domain.Dtos;
using FluentValidation;

namespace Services
{
    public class RegistrationValidator : AbstractValidator<RegistrationDto> 
    {
        public RegistrationValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(dto => dto.Email.ToLower())
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid Email Address")
            .MustAsync(async (email, cancellation) =>
            {
                // Check if the email exists in the database
                return !await unitOfWork.UserRepository.EmailExists(email);
            })
            .WithMessage("Email already exists");

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
