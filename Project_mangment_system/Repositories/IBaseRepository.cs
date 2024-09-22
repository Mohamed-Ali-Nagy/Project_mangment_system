using System.Linq.Expressions;

using Project_management_system.CQRS.Users.Commands;
using System.Linq.Expressions;
using Project_management_system.Specification;
using Project_management_system.Models;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<TResult> GetAsyncWithProjectTo<TResult>(Expression<Func<TResult, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        public void Add(T entity);
        void SaveChanges();
		Task<IEnumerable<T>> GetAsyncWithSpec(Expression<Func<T, bool>> expression);
		System.Threading.Tasks.Task AddAsync(T entity);
		Task<int> SaveChangesAsync();
		Task<T?> GetByIdAsync(int id);
		Task<int> GetCountWithSpecAsync(IBaseSpecification<T> baseSpecification);
		Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> Spec);
		Task<IEnumerable<T>> Get_Async(Expression<Func<T, bool>> expression);
	}
}
