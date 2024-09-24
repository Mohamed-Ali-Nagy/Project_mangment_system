using Project_management_system.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.ViewModels.Task
{
    public class UpdateTaskVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public int? UserID { get; set; }
    }
}
