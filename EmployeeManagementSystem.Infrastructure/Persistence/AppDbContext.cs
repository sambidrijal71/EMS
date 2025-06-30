using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployee(modelBuilder);
            ConfigureUser(modelBuilder);
        }

        private static void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
{
    entity.OwnsOne(e => e.Email, email =>
    {
        email.Property(e => e.Value).HasColumnName("Email").IsRequired();
    });
    entity.OwnsOne(p => p.PhoneNumber, phoneNumber =>
    {
        phoneNumber.Property(p => p.Value).HasColumnName("PhoneNumber").IsRequired();
    });
    entity.OwnsOne(d => d.DateOfBirth, dateOfBirth =>
    {
        dateOfBirth.Property(p => p.Value).HasColumnName("DateOfBirth").IsRequired();
    });
    entity.OwnsOne(f => f.FullName, name =>
    {
        name.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
        name.Property(p => p.LastName).HasColumnName("LastName").IsRequired();
    });
});
            modelBuilder.Entity<Employee>().Property(d => d.Department).HasConversion<string>();
            modelBuilder.Entity<Employee>().Property(e => e.ActivityStatus).HasConversion<string>();
            modelBuilder.Entity<Employee>().Property(e => e.Status).HasConversion<string>();
            modelBuilder.Entity<Employee>().Property(p => p.Position).HasConversion<string>();
        }

        private static void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
{
    entity.OwnsOne(u => u.Username, username =>
    {
        username.Property(e => e.Value).HasColumnName("Username").IsRequired();
    });
    entity.OwnsOne(e => e.Email, email =>
    {
        email.Property(e => e.Value).HasColumnName("Email").IsRequired();
    });
    entity.OwnsOne(p => p.Password, password =>
    {
        password.Property(p => p.Value).HasColumnName("Password");
    });
});
            modelBuilder.Entity<User>().Property(p => p.Role).HasConversion<string>();
        }
    }

}