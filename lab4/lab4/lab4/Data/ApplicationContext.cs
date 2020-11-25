using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace lab2
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Train> Trains { get; set; }
        public string connectionString { get; set; }
        public IConfiguration Configuration { get; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //connectionString = Configuration.GetConnectionString("SqlServerConnection");
            connectionString = "Server=DESKTOP-OI4GN8K\\SQLEXPRESS;Database=Course;Trusted_Connection=True;MultipleActiveResultSets=true";    
        }
    }
}
