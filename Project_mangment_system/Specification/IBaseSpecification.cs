using Microsoft.EntityFrameworkCore.Query;
using Project_management_system.Models;
using System.Linq.Expressions;

namespace Project_management_system.Specification
{
	public interface IBaseSpecification<T> where T : BaseModel
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; }
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }
	}
}
