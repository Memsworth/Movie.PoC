using Movie.PoC.Api.Contracts.DTOs;

namespace Movie.PoC.Api.Contracts;

public interface ITokenGeneratorService
{
    string GenerateToken(UserTokenDto userData);
    bool ValidateToken(string token);
}