using Microsoft.EntityFrameworkCore;
using Project_management_system.Data;
using Project_management_system.Models;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task SaveOtpAsync(Guid userId, string otp)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePasswordAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateOtpAsync(Guid userId, string otp)
        {
            throw new NotImplementedException();
        }
    }
}
