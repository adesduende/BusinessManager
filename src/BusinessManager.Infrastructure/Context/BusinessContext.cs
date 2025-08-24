using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Primitives;
using BusinessManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System; // For Guid

namespace BusinessManager.Infrastructure.Context
{
    public class BusinessContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BusinessContext(DbContextOptions<BusinessContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Configure Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Description).IsRequired().HasMaxLength(500);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Surname).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Nif).IsRequired().HasMaxLength(10).HasConversion((Nif e) => e.Value, (string s) => new Nif(s));
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255).HasConversion((Email e) => e.Value, (string s) => new Email(s));
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.HasMany(r => r.Roles)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("UserRoles"));

                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Nif).IsUnique();
            });
            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).IsRequired();
                entity.Property(o => o.createdAt).IsRequired();
                entity.Property(o => o.updatedAt).IsRequired();
                entity.Property(o => o.Description).IsRequired().HasMaxLength(500);
                entity.Property(o => o.Status).IsRequired().HasConversion<string>();
            });

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Surname).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(255).HasConversion(e => e.Value, s => new Email(s));
                entity.Property(c => c.Nif).IsRequired().HasMaxLength(10).HasConversion(e => e.Value, s => new Nif(s));
                entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.ComplexProperty(c => c.Address, a =>
                {
                    a.Property(ad => ad.Street).IsRequired().HasMaxLength(200).HasColumnName("Street");
                    a.Property(ad => ad.City).IsRequired().HasMaxLength(100).HasColumnName("City");
                    a.Property(ad => ad.State).IsRequired().HasMaxLength(100).HasColumnName("State");
                    a.Property(ad => ad.ZipCode).IsRequired().HasMaxLength(20).HasColumnName("ZipCode");
                    a.Property(ad => ad.Country).IsRequired().HasMaxLength(100).HasColumnName("Country");
                });

                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.Nif).IsUnique();
            });

            //Configure Orders Per User  Many-TO-Many
            modelBuilder.Entity<User>()
                .HasMany<Order>()
                .WithMany(o => o.Technicians)
                .UsingEntity(j => j.ToTable("OrdersPerUser"))
                .HasKey(u => u.Id);
                

        }

    }

}
