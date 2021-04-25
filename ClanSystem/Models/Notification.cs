using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClanSystem.Models
{
    public enum Importance
    {
        Low, Default, High
    }

    public class Notification
    {
        public long ID { get; set; }
        [Required]
        public DateTime SendTime { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public Importance? Importance { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public string AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public ApplicationUser Author { get; set; }
        [InverseProperty("Notifications")]
        public virtual ICollection<ApplicationUser> UserRecipients { get; set; }
        [InverseProperty("Notifications")]
        public virtual ICollection<Group> GroupRecipients { get; set; }

    }
}