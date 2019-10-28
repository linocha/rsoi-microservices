using Microsoft.EntityFrameworkCore;
using Subscribes.Domain.Models;

namespace Subscribes.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Subscribe> Subscribes { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscribe>().ToTable("Subscribes-Info", "subscribes");
            modelBuilder.Entity<Subscribe>().HasKey(p => p.Id);
            modelBuilder.Entity<Subscribe>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Subscribe>().Property(p => p.ExtUserId).IsRequired();
            modelBuilder.Entity<Subscribe>().Property(p => p.ExtProdId).IsRequired();
            modelBuilder.Entity<Subscribe>().Property(p => p.DataStart).IsRequired();
            modelBuilder.Entity<Subscribe>().Property(p => p.DataEnd).IsRequired();
            
        }
    }
}