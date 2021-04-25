namespace ClanSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCraete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        SendTime = c.DateTime(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Importance = c.Int(),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        AuthorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorizationTokens",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Value = c.Guid(nullable: false),
                        ExpirationDate = c.DateTime(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.NotificationApplicationUsers",
                c => new
                    {
                        Notification_ID = c.Long(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Notification_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Notifications", t => t.Notification_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Notification_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.GroupNotifications",
                c => new
                    {
                        Group_ID = c.Long(nullable: false),
                        Notification_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.Notification_ID })
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.Notifications", t => t.Notification_ID, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.Notification_ID);
            
            CreateTable(
                "dbo.GroupApplicationUsers",
                c => new
                    {
                        Group_ID = c.Long(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorizationTokens", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.GroupNotifications", "Notification_ID", "dbo.Notifications");
            DropForeignKey("dbo.GroupNotifications", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.NotificationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationApplicationUsers", "Notification_ID", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "AuthorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GroupApplicationUsers", new[] { "Group_ID" });
            DropIndex("dbo.GroupNotifications", new[] { "Notification_ID" });
            DropIndex("dbo.GroupNotifications", new[] { "Group_ID" });
            DropIndex("dbo.NotificationApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.NotificationApplicationUsers", new[] { "Notification_ID" });
            DropIndex("dbo.AuthorizationTokens", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.Notifications", new[] { "AuthorID" });
            DropTable("dbo.GroupApplicationUsers");
            DropTable("dbo.GroupNotifications");
            DropTable("dbo.NotificationApplicationUsers");
            DropTable("dbo.AuthorizationTokens");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Notifications");
            DropTable("dbo.Groups");
        }
    }
}
