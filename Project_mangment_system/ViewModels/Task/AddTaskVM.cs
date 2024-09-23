using Project_management_system.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.Task
{
    public class AddTaskVM
    {
        [Required]
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
    }
}

