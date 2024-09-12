using Project_management_system.Models;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<User?> GetByEmailAsync(string email);
        Task SaveOtpAsync(Guid userId, string otp);
        Task<bool> ValidateOtpAsync(Guid userId, string otp);
        Task UpdatePasswordAsync(User user);
    }
}
