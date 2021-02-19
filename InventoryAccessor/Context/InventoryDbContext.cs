using Accessors.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accessors.Context
{
	public class InventoryDbContext : DbContext
	{
		public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Order> Orders { get; set; }

		public virtual DbSet<Customer> Customers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
            {
				optionsBuilder.UseLazyLoadingProxies();
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Configure Tables
			modelBuilder.Entity<Order>().ToTable("Orders");
			modelBuilder.Entity<Customer>().ToTable("Customers");

			// Configure Primary Keys  
			modelBuilder.Entity<Order>().HasKey(order => order.Id).HasName("PK_Order");
			modelBuilder.Entity<Customer>().HasKey(customer => customer.Id).HasName("PK_Customer");

			// Configure columns for Orders
			modelBuilder.Entity<Order>().Property(order => order.Id).HasColumnType("int").UseIdentityColumn().IsRequired();
			modelBuilder.Entity<Order>().Property(order => order.Name).HasColumnType("nvarchar(100)").IsRequired();
			modelBuilder.Entity<Order>().Property(order => order.DateCreated).HasColumnType("datetime").IsRequired();
			modelBuilder.Entity<Order>().Property(order => order.DateLastModified).HasColumnType("datetime").IsRequired();
			modelBuilder.Entity<Order>().Property(order => order.CustomerId).HasColumnType("int").IsRequired();

			// Configure column for Customers
			modelBuilder.Entity<Customer>().Property(customer => customer.Id).HasColumnType("int").UseIdentityColumn().IsRequired();
			modelBuilder.Entity<Customer>().Property(customer => customer.Name).HasColumnType("nvarchar(100)").IsRequired();

			// Configure Foreigh Key between Orders and Customers  
			modelBuilder.Entity<Order>()
				.HasOne<Customer>()
				.WithMany()
				.HasPrincipalKey(customer => customer.Id)
				.HasForeignKey(order => order.CustomerId)
				.OnDelete(DeleteBehavior.NoAction)
				.HasConstraintName("FK_Orders_Customers");
		}
	}
}
