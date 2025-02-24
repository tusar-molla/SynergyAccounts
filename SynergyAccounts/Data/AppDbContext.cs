using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Models;

namespace SynergyAccounts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }       
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubscriptionModule> SubscriptionModules { get; set; }        
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding the Role data
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "SuperAdmin" },
                new Role { Id = 2, Name = "Operator" }
            );

            // Seeding the User data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Super Admin",
                    UserName = "superadmin007",
                    Email = "admin@gmail.com",
                    PasswordHash = "123456",
                    ContactNumber = 01817171283,
                    Address = "Dhanmondi",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FullName = "Operator",
                    UserName = "operator007",
                    Email = "operator@gmail.com",
                    PasswordHash = "123456",
                    ContactNumber = 01910126335,
                    Address = "Mohammadpur",
                    RoleId = 2
                }
            );
        }
        }
}
