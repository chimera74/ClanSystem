using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClanSystem.Models
{
    public class AuthorizationToken
    {
        public long ID { get; set; }
        [Required]
        public Guid Value { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        [Required]
        public Guid DeviceID { get; set; }
    }
}