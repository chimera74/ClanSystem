namespace ClanSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthorizationTokens", "DeviceID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuthorizationTokens", "DeviceID");
        }
    }
}
