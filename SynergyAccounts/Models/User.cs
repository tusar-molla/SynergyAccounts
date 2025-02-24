using System.Numerics;

namespace SynergyAccounts.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? ContactNumber { get; set; }
        public string? Address { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
