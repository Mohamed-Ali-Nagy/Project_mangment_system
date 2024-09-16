using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Models;
using static Project_management_system.CQRS.Users.Queries.GetUserByPredicateQueryHandler;

namespace Project_management_system.Services.TokenGenerator
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserDetailsDTO user);
    }
}
