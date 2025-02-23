namespace SynergyAccounts.Models
{
    public class SubscriptionModule
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
