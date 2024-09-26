using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NEGOSUD.Models.Entities;

namespace NEGOSUD.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderDetail> SupplierOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category-Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // One Product has one Category
                .WithMany(c => c.Products) // One Category has many Products
                .HasForeignKey(p => p.CategoryID) // Foreign Key in Product table
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Category is deleted

            // Configure Supplier-Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products) // Assumes Supplier can have many Products
                .HasForeignKey(p => p.SupplierID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete if Supplier is deleted

            // Configure Customer-Order relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order-OrderDetail relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Product-OrderDetail relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced in an OrderDetail

            // Configure Supplier-SupplierOrder relationship
            modelBuilder.Entity<SupplierOrder>()
                .HasOne(so => so.Supplier)
                .WithMany(s => s.SupplierOrders)
                .HasForeignKey(so => so.SupplierID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure SupplierOrder-SupplierOrderDetail relationship
            modelBuilder.Entity<SupplierOrderDetail>()
                .HasOne(sod => sod.SupplierOrder)
                .WithMany(so => so.SupplierOrderDetails)
                .HasForeignKey(sod => sod.SupplierOrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Product-SupplierOrderDetail relationship
            modelBuilder.Entity<SupplierOrderDetail>()
                .HasOne(sod => sod.Product)
                .WithMany(p => p.SupplierOrderDetails)
                .HasForeignKey(sod => sod.ProductID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced in a SupplierOrderDetail
        }

    }
}
