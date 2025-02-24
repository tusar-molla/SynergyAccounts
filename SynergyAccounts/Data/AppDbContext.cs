using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Models;

namespace SynergyAccounts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissionss { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubscriptionModule> SubscriptionModules { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            var adminRoleId = 1;
            //var userRoleId = 2;

            // Static ID for admin user (since UserId is INT, use fixed integer)
            var adminId = 1;

            //  Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin" }
                //new Role { Id = userRoleId, Name = "User" }
            );

            //  Seed default admin user
            modelBuilder.Entity<User>().HasData(
                new User { Id = adminId, Email = "admin@.com", PasswordHash = "admin123", FullName = "Super Admin", BranchId = null }
            );

            //  Assign admin user to "Admin" role
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = adminId, RoleId = adminRoleId }
            );
        }


    }
}
