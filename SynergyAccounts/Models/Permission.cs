namespace SynergyAccounts.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Example: "View Reports", "Manage Users", "Create Invoice"

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
