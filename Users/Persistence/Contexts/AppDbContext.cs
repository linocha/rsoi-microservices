using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;

namespace Users.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users-Info", "users");
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.FirstName);
            modelBuilder.Entity<User>().Property(p => p.LastName);

            modelBuilder.Entity<User>().HasData(
                new User {Id = 100, Email = "lalala@llalala.com"},
                new User {Id = 100, Email = "mamamama@mamamama.com"}
            );
        }
    }
}