using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Data;
using Project_management_system.Data;
using Project_management_system.Models;
using Project_management_system.Specification;
using System.Linq;
using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public BaseRepository(Context context ,IMapper mapper)
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
            var result =  GetAll().ProjectTo<TResult>(_mapper.ConfigurationProvider);
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

        public async Task<IEnumerable<T>> GetAsyncWithSpec(Expression<Func<T, bool>> expression)
		{
			var query = _context.Set<T>().AsQueryable();
			query = query.Where(x => !x.IsDeleted);
			query = query.Where(expression);
			return await query.ToListAsync();
		}

		public async System.Threading.Tasks.Task AddAsync(T entity)
		{
			await _context.AddAsync(entity);
		}

		public async Task<int> SaveChangesAsync()
		{
			  return await _context.SaveChangesAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().Where(x=>!x.IsDeleted).FirstOrDefaultAsync(x=>x.ID==id);
		}

		public async Task<int> GetCountWithSpecAsync(IBaseSpecification<T> baseSpecification)
		{
		  return await ApplySpecification(baseSpecification).Where(x=>!x.IsDeleted).CountAsync();
		}

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> Spec)
		{
			return await ApplySpecification(Spec).Where(x=>!x.IsDeleted).ToListAsync();
		}
		private IQueryable<T> ApplySpecification(IBaseSpecification<T> Spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), Spec);
		}

		public async Task<IEnumerable<T>> Get_Async(Expression<Func<T, bool>> expression)
		{
			var query = _context.Set<T>().AsQueryable();
			query = query.Where(x => !x.IsDeleted);
			query = query.Where(expression);
			return await query.ToListAsync();
		}
	}
}

