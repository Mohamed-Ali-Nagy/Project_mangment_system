using Project_management_system.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.ViewModels.Task
{
    public class TaskDetailsVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string UserName {  get; set; }
    }
}
