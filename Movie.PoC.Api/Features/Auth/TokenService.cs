using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.DTOs;
using Movie.PoC.Api.Settings;

namespace Movie.PoC.Api.Features.Auth;


//CREDIT GOES TO TheRealMKB who showed me how how to write jwt webtokens

public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;

    public TokenService(IOptions<JwtConfig> jwtConfig) => _jwtConfig = jwtConfig.Value;
    
    public string GenerateToken(UserTokenDto userData)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userData.Name),
            new Claim(ClaimTypes.Email, userData.Email),
            new Claim(ClaimTypes.NameIdentifier, userData.Id),
            new Claim(ClaimTypes.Role, userData.Role)
        };
        
        //fix this bug later
        var expiryMins = _jwtConfig.ExpiryMins;

        return BuildToken(claims, 10);
    }

    private string BuildToken(IEnumerable<Claim> claims, int expiryMins) 
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            expires: DateTime.Now.AddMinutes(expiryMins),
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    
    public bool ValidateToken(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Key));
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                IssuerSigningKey = securityKey
            }, out SecurityToken validatedToken);

            return validatedToken.ValidTo >= DateTime.Now;
        }
        catch (Exception e)
        {
        }

        return false;
    }

    
}