using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project_management_system.Helpers;
using Project_management_system.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_management_system.Services
{
    public class GenerateTokenService
    {
        public static string GenerateToken(User user)
        {
            var authClaims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.Name),
                  new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),


            };
            foreach (var userRole in user.UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.ToString()));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = ConfigHelper.GetAppSetting("jwtSettings:Issuer"),
                Audience = ConfigHelper.GetAppSetting("jwtSettings:Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigHelper.GetAppSetting("jwtSettings:Key"))), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
