namespace Project_management_system.ViewModels.TaskVM
{
	public class AddTaskViemModel
	{
        public string Title { get; set; }
        public int ProjectId { get; set; }

        public int? AssignedToUserId { get; set; }
    }
}
