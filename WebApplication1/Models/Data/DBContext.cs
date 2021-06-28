using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.discountCode)
                .IsFixedLength();

            modelBuilder.Entity<Discount>()
                .Property(e => e.value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Discount>()
                .Property(e => e.conditionMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Discount>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.reduceMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.totalMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.amountMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.productPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.productDetail)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.image)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.roleName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.userName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.phoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.image)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
