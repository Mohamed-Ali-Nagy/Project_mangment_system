using Project_management_system.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.UserVMs
{
    public class UserRegisterVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public string Country { get; set; }

    }
}
