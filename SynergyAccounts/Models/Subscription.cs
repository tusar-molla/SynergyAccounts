namespace SynergyAccounts.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public ICollection<SubscriptionModule> SubscriptionModules { get; set; } = new List<SubscriptionModule>();
    }
}
