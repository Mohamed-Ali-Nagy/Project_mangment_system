namespace Project_management_system.Enums
{
	public class TaskError
	{
		public static readonly Error TaskNotFound =
	 new("Task is not found", StatusCodes.Status404NotFound);

		public static readonly Error UserIsAlreadyAssignedToThisTask =
			new("User is not assigned to this Task", StatusCodes.Status409Conflict);

	}
}
