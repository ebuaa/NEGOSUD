using Negosud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Negosud.Data
{
    public class NegosudContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderDetail> SupplierOrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=ALI;Initial Catalog=Negosud;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.PricePerUnit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SupplierOrderDetail>()
                .Property(sod => sod.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SupplierOrderDetail>()
                .Property(sod => sod.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SupplierOrderDetail>()
                .HasOne(sod => sod.SupplierOrder)
                .WithMany(so => so.SupplierOrderDetails)
                .HasForeignKey(sod => sod.SupplierOrderID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<SupplierOrderDetail>()
                .HasOne(sod => sod.Product)
                .WithMany(p => p.SupplierOrderDetails)
                .HasForeignKey(sod => sod.ProductID)
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
