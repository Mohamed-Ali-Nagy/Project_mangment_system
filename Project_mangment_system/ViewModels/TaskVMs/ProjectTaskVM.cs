using Project_management_system.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.ViewModels.TaskVMs
{
    public class ProjectTaskVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string UserName { get; set; }
      
    }
}
