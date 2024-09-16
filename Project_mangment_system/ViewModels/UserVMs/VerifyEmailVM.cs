using System.ComponentModel.DataAnnotations;

namespace Project_management_system.ViewModels.UserVMs
{
    public class VerifyEmailVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int OTPCode { get; set; }
    }
}
