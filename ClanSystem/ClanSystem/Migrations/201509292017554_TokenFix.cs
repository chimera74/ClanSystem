namespace ClanSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuthorizationTokens", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.AuthorizationTokens", new[] { "UserID" });
            AlterColumn("dbo.AuthorizationTokens", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AuthorizationTokens", "DeviceID", c => c.Guid(nullable: false));
            CreateIndex("dbo.AuthorizationTokens", "UserID");
            AddForeignKey("dbo.AuthorizationTokens", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorizationTokens", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.AuthorizationTokens", new[] { "UserID" });
            AlterColumn("dbo.AuthorizationTokens", "DeviceID", c => c.String());
            AlterColumn("dbo.AuthorizationTokens", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AuthorizationTokens", "UserID");
            AddForeignKey("dbo.AuthorizationTokens", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
