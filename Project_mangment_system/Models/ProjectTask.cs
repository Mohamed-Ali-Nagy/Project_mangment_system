using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.Models
{
    public class ProjectTask : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User? User { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
