using Microsoft.EntityFrameworkCore;

namespace Project_management_system.Specification.TaskSpec
{
	public class TaskSpec:BaseSpecification<Models.Task>
	{
		public TaskSpec(SpecParams spec)
		{
			Includes.Add(p => p.Include(p => p.User)!);
			Includes.Add(p => p.Include(p => p.Project)!);

			if (!string.IsNullOrEmpty(spec.Search))
			{
				Criteria = p => p.Title.ToLower().Contains(spec.Search.ToLower());
			}

			if (!string.IsNullOrEmpty(spec.Sort))
			{
				switch (spec.Sort.ToLower())
				{
					case "title":
						AddOrderBy(p => p.Title);
						break;
					default:
						AddOrderBy(p => p.CreatedOn);
						break;
				}
			}

			ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
		}
	}
}
