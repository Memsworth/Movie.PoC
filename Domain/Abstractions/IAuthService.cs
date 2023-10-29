using Domain.Dtos;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        public Task Register(RegistrationDto userRegistration);
    }
}
