using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.UserVMs
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
