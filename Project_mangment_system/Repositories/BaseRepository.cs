using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Data;
using Project_management_system.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public BaseRepository(Context context,IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }
        public   IQueryable<T>GetAll()
        {

            return  _context.Set<T>().Where(T => !T.IsDeleted);
        }
        public  async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }
        public async Task<TResult> GetAsyncWithProjectTo<TResult>(Expression<Func<TResult, bool>> predicate)
        {
            var result =  GetAll().ProjectTo<TResult>(_mapper.ConfigurationProvider);
               return await result.FirstOrDefaultAsync(predicate);
        }
        //public async Task< IEnumerable<TResult>> Select<TResult>(Expression<Func<T, TResult>> selector)
        //{
        //    return await GetAll().Select(selector).ToListAsync();
        //}



    }
}
