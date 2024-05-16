using E_CommerceFurnitureBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceFurnitureBackend.DbCo
{
    public class UserDbContext:DbContext
    {
        private readonly string _connectionString;
        public UserDbContext(IConfiguration configuration)
        {
            this._connectionString = configuration["ConnectionStrings:connection"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Cart>()
                .HasOne(u => u.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.UserId);
            modelBuilder.Entity<Order>()
                .HasOne(u=>u.User)
                .WithMany(o=>o.Order)
                .HasForeignKey(o=>o.CustomerId);
            modelBuilder.Entity<CartItems>()
                .HasOne(c=>c.Cart)
                .WithMany(ci=>ci.CartItems)
                .HasForeignKey(ci=>ci.CartId);
            modelBuilder.Entity<CartItems>()
                .HasOne(p => p.Product)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(ci => ci.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(cg => cg.categories)
                .WithMany(p => p.Product)
                .HasForeignKey(p=>p.Category);
            modelBuilder.Entity<Product>()
                .HasOne(t=>t.types)
                .WithMany(p=>p.Product)
                .HasForeignKey(p=>p.Type);
            modelBuilder.Entity<OrderItem>()
                .HasOne(p=>p.Product)
                .WithMany(oi=>oi.OrderItems)
                .HasForeignKey(oi=>oi.ProductId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(o=>o.order)
                .WithMany(oi=>oi.orderItems)
                .HasForeignKey(oi=>oi.OrderId);
        }
        public DbSet<User> Users { get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<Cart> Cart { get; set;}
        public DbSet <CartItems> CartItems { get; set;}
        public DbSet<OrderItem> OrderItems { get; set;}
        public DbSet<Order> Order {  get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<Types> Types { get; set;}

    }
}
