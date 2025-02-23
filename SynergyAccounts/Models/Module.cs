namespace SynergyAccounts.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Example: "Inventory", "Accounting"

        public ICollection<SubscriptionModule> SubscriptionModules { get; set; } = new List<SubscriptionModule>();
    }
}
