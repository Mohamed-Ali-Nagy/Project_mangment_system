using Project_management_system.Enums;
using Project_management_system.Models;

namespace Project_management_system.ViewModels.ProjectVMs
{
    public class CreateProjectVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; } 
        public ProjectStatus Status { get; set; }
        public int UserID { get; set; }
       
    }
}
