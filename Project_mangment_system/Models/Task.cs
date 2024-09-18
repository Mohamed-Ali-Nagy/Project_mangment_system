using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.Models
{
    public class Task:BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
