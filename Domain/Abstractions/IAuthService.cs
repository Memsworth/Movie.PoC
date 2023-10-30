using Domain.Dtos;
using Domain.Models;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        public Task<BaseServiceResponse?> Register(RegistrationDto userRegistration);
    }
}
