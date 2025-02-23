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


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Static GUIDs for roles
        //    var adminRoleId = new Guid("f47a9b25-3d0b-45ff-bc6a-8d0ad4f5e01e");
        //    var userRoleId = new Guid("50c4d34b-c548-44f0-b2f5-401440e45ab1");

        //    // Static GUID for the admin user
        //    var adminId = new Guid("53e3998d-38f0-4659-b02f-6ab056f49ea7");

        //    // Seed roles
        //    modelBuilder.Entity<Role>().HasData(
        //        new Role { Id = adminRoleId, Name = "Admin" },
        //        new Role { Id = userRoleId, Name = "User" }
        //    );

        //    // Seed default user
        //    modelBuilder.Entity<User>().HasData(
        //        new User { Id = adminId, Email = "admin@example.com", PasswordHash = "admin123" }
        //    );

        //    // Assign admin user to "Admin" role
        //    modelBuilder.Entity<UserRole>().HasData(
        //        new UserRole { Id = Guid.NewGuid(), UserId = adminId, RoleId = adminRoleId }
        //    );
        //}
    }
}
