using Domain.Dtos;

namespace Domain.Abstractions
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserInfoDto userData);
        bool ValidateToken(string token);
    }
}
