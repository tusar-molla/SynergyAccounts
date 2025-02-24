using System.ComponentModel.DataAnnotations;

namespace SynergyAccounts.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Address { get; set; }

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
