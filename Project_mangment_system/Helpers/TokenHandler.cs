using Microsoft.IdentityModel.Tokens;
using Project_management_system.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_management_system.Helpers
{
    public static class TokenHandler
    {
        public static string GenerateToken(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
            };


            //foreach (var userRole in user.Roles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                 new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),
                }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(Constants.DurationInMinutes),
                Issuer = Constants.Issuer,
                Audience = Constants.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
