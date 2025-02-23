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
    }
}
