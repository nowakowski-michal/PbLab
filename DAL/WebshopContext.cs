using Microsoft.EntityFrameworkCore;
using Model;
using System.Reflection.Metadata;

namespace DAL
{
    public class WebshopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<BasketPosition> BasketPositions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPosition> OrderPositions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder
      optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PBTest4;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasMany(g => g.Users).WithOne(u => u.Group).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<ProductGroup>().HasMany(g => g.Products).WithOne(p => p.Group).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ProductGroup>().HasOne(p => p.Parent).WithMany(r => r.Childrens).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BasketPosition>().HasOne(b => b.Product).WithMany(p => p.BasketPositions).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BasketPosition>().HasOne(b => b.User).WithMany(u => u.BasketPositions).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(u => u.Orders).WithOne(o => o.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().HasMany(o => o.Positions).WithOne(p => p.Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}