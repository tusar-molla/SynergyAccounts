﻿using System.ComponentModel.DataAnnotations;

namespace SynergyAccounts.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
