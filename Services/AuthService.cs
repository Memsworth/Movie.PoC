using Domain.Abstractions;
using Domain.Dtos;
using Domain.Models;
using Services.Helper;

namespace Services
{
    public class AuthService : IAuthService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<BaseServiceResponse?> Register(RegistrationDto userRegistration)
        {
            //fix this later
            if (userRegistration is null) throw new Exception("registration is null");
            
            var email = await ValidationService.EmailExists(userRegistration.Email, _unitOfWork);

            if (email)
            {
                return new BaseServiceResponse()
                {
                    Message = "This email is already associated with an account."
                };
            }
            return null;
        }

        public async Task<BaseServiceResponse?> Login(LoginDto loginDto)
        {
            var email = await ValidationService.EmailExists(loginDto.Email, _unitOfWork);

            if (email is false)
                return new BaseServiceResponse
                {
                    Message = "There is not account associated with this email"
                };

            if (EncryptionHelper.VerifyPassword(loginDto.Password, ))
            {

            }
            return null;
        }
    }
}
