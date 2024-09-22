using Project_management_system.Models;
using System.Linq;

namespace Project_management_system.Specification
{
	public static class SpecificationEvaluator<T> where T : BaseModel
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IBaseSpecification<T> spec)
		{
			var query = inputQuery;


			if (spec.Criteria is not null)
			{
				query = query.Where(spec.Criteria);
			}

			if (spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy);
			}

			if (spec.OrderByDesc is not null)
			{
				query = query.OrderByDescending(spec.OrderByDesc);
			}
			if (spec.IsPaginationEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take);
			}

			query = spec.Includes.Aggregate(query, (current, include) => include(current));
			return query;
		}

	}
}
