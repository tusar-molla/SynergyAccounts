﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SynergyAccounts.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public ICollection<User>? Users { get; set; }
    }
}
