using Project_management_system.Data;
using Project_management_system.Models;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T>:IBaseRepository<T> where T :BaseModel
    {
        private Context _context;
        public BaseRepository(Context context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll() 
        {
           return _context.Set<T>();
        }
    }
}
