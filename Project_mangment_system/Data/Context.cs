using Microsoft.EntityFrameworkCore;
using Project_management_system.Models;

namespace Project_management_system.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options):base(options) { }
        
            
        
        public DbSet<User> Users { get; set; }
        public DbSet<Models.ProjectTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectsUsers> ProjectsUsers { get; set; }
    }
}
