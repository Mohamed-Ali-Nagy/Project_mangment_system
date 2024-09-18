namespace Project_management_system.Helpers
{
    public static class OTPHelper
    {
        public static string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }  
        public static DateTime SetOtpExpiry()
        {
            return DateTime.Now.AddMinutes(5);
        }

    }
}
