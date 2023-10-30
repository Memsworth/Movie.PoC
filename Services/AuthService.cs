using Domain.Abstractions;
using Domain.Dtos;
using Domain.Models;

namespace Services
{
    public class AuthService : IAuthService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task Register(RegistrationDto userRegistration)
        {
            //fix this later
            if (userRegistration is null) throw new Exception("registration is null");
            var email = (await _unitOfWork.UserRepository.GetAsync()).FirstOrDefault(x => x.Email == userRegistration.Email);


            //This is where the meat is
            await _unitOfWork.UserRepository.AddAsync(new User
            {
                Email = userRegistration.Email.ToLower(),
                HashedPassowrd = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password),
                Name = userRegistration.Name,
                DateOfBirth = userRegistration.DateOfBirth,
                CreatedDate = DateTime.Now,
                Role = Role.User
            });

            await _unitOfWork.CommitAsync();
        }
    }
}
