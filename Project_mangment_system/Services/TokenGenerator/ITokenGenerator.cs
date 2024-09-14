using Project_management_system.Models;

namespace Project_management_system.Services.TokenGenerator
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
