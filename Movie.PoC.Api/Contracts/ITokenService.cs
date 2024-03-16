using Movie.PoC.Api.Contracts.DTOs;

namespace Movie.PoC.Api.Contracts;

public interface ITokenService
{
    string GenerateToken(UserTokenDto userData);
    bool ValidateToken(string token);
}