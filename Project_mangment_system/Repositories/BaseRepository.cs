using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Project_management_system.Data;
using Project_management_system.Models;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public BaseRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public async Task<TResult> GetAsyncWithProjectTo<TResult>(Expression<Func<TResult, bool>> predicate)
        {
            var result = GetAll().ProjectTo<TResult>(_mapper.ConfigurationProvider);
            return await result.FirstOrDefaultAsync(predicate);
            //return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAll()
            => _context.Set<T>().Where(e => !e.IsDeleted);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
             => GetAll().Where(predicate);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }  
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
        }  
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
        public async Task<T>  GetByID(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}

