using Domain.Abstractions;
using Domain.Dtos;
using Domain.Models;

namespace Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork) {  _unitOfWork = unitOfWork; }
        public async Task Register(RegistrationDto userRegistration)
        {
            await _unitOfWork.UserRepository.AddAsync(new User
            {
                Email = userRegistration.Email.ToLower(),
                HashedPassowrd = Helper.EncryptionHelper.GeneratePassword(userRegistration.Password),
                Name = userRegistration.Name,
                DateOfBirth = userRegistration.DateOfBirth,
                CreatedDate = DateTime.Now,
                Role = Role.User
            });
            await _unitOfWork.CommitAsync();
        }


    }
}
