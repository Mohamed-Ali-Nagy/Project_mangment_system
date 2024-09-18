using Project_management_system.Enums;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.Models
{
    public class Project:BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description  { get; set; }
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public ProjectStatus Status { get; set; }
        public List<Task> Tasks { get; set; }
        public List<ProjectsUsers> Users { get; set; }

    }
}
