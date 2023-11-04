using Domain.Abstractions;
using Domain.Configurations;
using Domain.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtConfig _jwtConfig;
        public TokenGenerator(JwtConfig jwtConfig) => _jwtConfig = jwtConfig;
        
        public string GenerateToken(UserInfoDto userData)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userData.Name),
                new Claim(ClaimTypes.Email, userData.Email),
                new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                new Claim(ClaimTypes.Role, userData.Role.ToString())    
            };
            var expiryMins = _jwtConfig.ExpiryMins;
            return "";
        }

        private string BuildToken(IEnumerable<Claim> claims, int expiryMinds) 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
            var 
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
