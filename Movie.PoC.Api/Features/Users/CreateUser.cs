using FluentValidation;
using MediatR;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;

namespace Movie.PoC.Api.Features.Users
{
    public record CreateUser(CreateUserRequest UserInfo) : IRequest<bool>;

    public class CreateUserHandler : IRequestHandler<CreateUser, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<CreateUserRequest> _validator;

        public CreateUserHandler(ApplicationDbContext dbContext, IValidator<CreateUserRequest> validator)
        {
            _dbContext = dbContext;
            _validator = validator;

        }

        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UserInfo, cancellationToken);

            if (!validationResult.IsValid)
            {
                return false;
            }

            var user = new UserModel
            {
                Name = request.UserInfo.Name,
                Email = request.UserInfo.Email,
                Password = request.UserInfo.Password,
                BirthDay = request.UserInfo.BirthDay,
                Role = Role.User
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
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
}
