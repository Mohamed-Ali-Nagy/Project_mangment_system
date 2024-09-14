using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Project_management_system.Enums;
using Project_management_system.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;

namespace Project_management_system.Services.TokenGenerator
{
    public class TokenGenerator:ITokenGenerator
    {
        IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  string GenerateToken(User user)
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
                Issuer = _configuration["jwtSettings:Issuer"] ,
                Audience = _configuration["jwtSettings:Audience"] ,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["jwtSettings:Key"])), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

      
    }
}
