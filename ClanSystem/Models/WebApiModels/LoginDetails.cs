using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanSystem.Models.WebApiModels
{
    public class LoginDetails
    {        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid DeviceID { get; set; }
    }
}