using Project_management_system.CQRS.Users.Commands;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    }
}
