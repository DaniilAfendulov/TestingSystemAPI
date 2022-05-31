using Microsoft.EntityFrameworkCore;

namespace TestingSystemAPI.Models.DataBase
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):
            base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
