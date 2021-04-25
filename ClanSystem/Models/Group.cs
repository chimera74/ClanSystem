using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClanSystem.Models
{
    public class Group
    {
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [InverseProperty("Groups")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
        [InverseProperty("GroupRecipients")]
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}