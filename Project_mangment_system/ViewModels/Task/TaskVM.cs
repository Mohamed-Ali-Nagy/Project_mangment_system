namespace Project_management_system.ViewModels.Task
{
    public class ProjectTaskVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UserName { get; set; }
    }
}
