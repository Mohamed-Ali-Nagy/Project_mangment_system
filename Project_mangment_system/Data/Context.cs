using Microsoft.EntityFrameworkCore;
using Project_mangment_system.Models;

namespace Project_mangment_system.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options):base(options) { }
        
            
        
        public DbSet<User> Users { get; set; }
    }
}
