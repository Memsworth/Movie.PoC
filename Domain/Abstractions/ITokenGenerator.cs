using Domain.Dtos;

namespace Domain.Abstractions
{
    public interface ITokenGenerator
    {
        string GenerateToken(RegistrationDto registrationDto);
        bool ValidateToken(string token);
    }
}
