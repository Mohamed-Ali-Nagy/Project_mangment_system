using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.UserVMs
{
    public class UserLoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
