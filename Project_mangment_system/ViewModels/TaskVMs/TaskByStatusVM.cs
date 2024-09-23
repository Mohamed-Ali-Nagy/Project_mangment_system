namespace Project_management_system.ViewModels.TaskVMs
{
    public class TaskByStatusVM
    {
        public string Status { get; set; }
        public IEnumerable<TaskVM> TaskVMs { get; set; } 
    }
}
