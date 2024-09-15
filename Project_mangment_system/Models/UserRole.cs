using Project_management_system.Enums;

namespace Project_management_system.Models
{
    public class UserRole:BaseModel
    {
        public int UserID { get; set; }
        public User User{ get; set; }

        public Role Role { get; set; }

    }
}
