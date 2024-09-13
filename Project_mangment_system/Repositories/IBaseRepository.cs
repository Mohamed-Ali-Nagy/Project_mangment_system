using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T>
    {
        public IQueryable<T> GetAll();
    }
}
