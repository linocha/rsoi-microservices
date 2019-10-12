using Microsoft.EntityFrameworkCore;
using Products.Domain.Models;

namespace Products.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Product>().Property(p => p.Cost).IsRequired();

            modelBuilder.Entity<Product>().HasData(
                new Product {Id = 100, Name = "Netflix", Cost = 1000},
                new Product {Id = 101, Name = "HBO", Cost = 500}
            );
        }
    }
}