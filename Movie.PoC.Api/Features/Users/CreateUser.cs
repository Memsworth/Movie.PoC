using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

            if (await IsEmailUnique(request.UserInfo.Email))
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

        private async Task<bool> IsEmailUnique(string email) => await _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    
}
