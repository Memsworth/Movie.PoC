using Domain.Abstractions;
using Domain.Configurations;
using Domain.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Helper
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

            return BuildToken(claims, expiryMins);
        }

        private string BuildToken(IEnumerable<Claim> claims, int expiryMins)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDiscriptor = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(expiryMins),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDiscriptor);
        }

        public bool ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var SecurityKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            if(tokenHandler.CanReadToken(token) == false) { return false; }
            if(tokenHandler.CanValidateToken == false) { return false; }
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = _jwtConfig.Audience,
                    ValidIssuer = _jwtConfig.Issuer,
                    IssuerSigningKey = SecurityKey
                }, out SecurityToken validatedToken);

            return validatedToken.ValidTo >= DateTime.Now;
        }
    }
}
