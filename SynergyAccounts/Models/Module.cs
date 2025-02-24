namespace SynergyAccounts.Models
{
    public class Module
    {
        public int Id { get; set; }
        public int ModulesUses { get; set; } // 1 account 2 inventory 3 both 
        public bool? IsDone { get; set; }

        public ICollection<SubscriptionModule> SubscriptionModules { get; set; } = new List<SubscriptionModule>();
    }
}
