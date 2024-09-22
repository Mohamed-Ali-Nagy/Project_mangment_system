namespace Project_management_system.Specification.TaskSpec
{
	public class CountTaskWithSepc:BaseSpecification<Models.Task>
	{
		public CountTaskWithSepc(SpecParams specParams)
	  : base(p => !p.IsDeleted)
		{

		}
	}
}
