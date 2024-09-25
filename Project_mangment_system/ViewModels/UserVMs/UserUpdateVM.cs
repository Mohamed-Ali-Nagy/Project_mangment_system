using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.UserVMs
{
    public class UserUpdateVM
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]

        public string PhoneNumber { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
