using Microsoft.IdentityModel.Tokens;
using Project_management_system.CQRS.Users.Queries;
using System.IdentityModel.Tokens.Jwt;
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
        public  string GenerateToken(UserDetailsDTO user)
        {
            var authClaims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.Name),
                  new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),


            };
            foreach (var userRole in user.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
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
