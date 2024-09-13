using System.ComponentModel.DataAnnotations.Schema;

namespace Project_management_system.Models
{
    public class OTP : BaseModel
    {

        public string OTPCode { get; set; }
        public DateTime OtpExpiry { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
