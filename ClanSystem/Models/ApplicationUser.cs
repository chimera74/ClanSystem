using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClanSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [InverseProperty("UserRecipients")]
        public virtual ICollection<Notification> Notifications { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<Notification> OwnedNotifications { get; set; }

        [InverseProperty("Users")]
        public virtual ICollection<Group> Groups { get; set; }
    }

    
}