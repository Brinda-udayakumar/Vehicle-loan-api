using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<UserLogin> UserLogin { get; set; }
    }
}