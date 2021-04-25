using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClanSystem.Models.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AzureSQLDBConnection")
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AuthorizationToken> Tokens { get; set; }

        //public System.Data.Entity.DbSet<ClanSystem.Models.ApplicationUser> IdentityUsers { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }
    }
}