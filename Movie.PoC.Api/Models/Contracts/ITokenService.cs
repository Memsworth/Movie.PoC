using Movie.PoC.Api.DTOs;

namespace Movie.PoC.Api.Models.Contracts;

public interface ITokenService
{
    string GenerateToken(UserTokenDto userData);
    bool ValidateToken(string token);
}