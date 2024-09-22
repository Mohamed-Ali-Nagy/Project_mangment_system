namespace Project_management_system.Enums
{
	public class ProjectError
	{
		public static readonly Error ProjectNotFound =
		new("Project Not Found", StatusCodes.Status404NotFound);
		public static readonly Error UserIsNotAssignedToThisProject =
		 new("User is not assigned to this project", StatusCodes.Status404NotFound);
	}
}
