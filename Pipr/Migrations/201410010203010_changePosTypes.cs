namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePosTypes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PostTypes", newName: "PosTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PosTypes", newName: "PostTypes");
        }
    }
}
