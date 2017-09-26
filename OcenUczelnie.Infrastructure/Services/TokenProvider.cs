using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Infrastructure.Services
{
    public class TokenProvider:ITokenProvider
    {
        private readonly JwtSettings _jwtSettings;

        public TokenProvider(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string CreateToken(Guid userId, string role)
        {
            var symetricKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role,role)
            };
            var token = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(15),
                signingCredentials:creds,
                notBefore:DateTime.UtcNow
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}