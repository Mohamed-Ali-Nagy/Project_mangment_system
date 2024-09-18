using System.Linq.Expressions;

using Project_management_system.CQRS.Users.Commands;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<TResult> GetAsyncWithProjectTo<TResult>(Expression<Func<TResult, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void SaveChanges();
    }
}
