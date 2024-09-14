using Microsoft.EntityFrameworkCore;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Data;
using Project_management_system.Models;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;  
        public BaseRepository(Context context)
        {
            _context = context; 
        }
        public  async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return  await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        

    }
}
