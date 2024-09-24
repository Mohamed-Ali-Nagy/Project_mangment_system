using Project_management_system.Enums;

namespace Project_management_system.ViewModels.ProjectVMs
{
    public class UpdateProjectVM
    {
        public int ID { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
