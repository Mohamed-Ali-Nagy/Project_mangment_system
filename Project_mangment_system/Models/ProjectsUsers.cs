using Microsoft.EntityFrameworkCore.Query;
using Project_management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.Models
{
    public class ProjectsUsers:BaseModel
    {
        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
