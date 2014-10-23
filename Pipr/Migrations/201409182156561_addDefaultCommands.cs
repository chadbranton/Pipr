namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDefaultCommands : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultCommands",
                c => new
                    {
                        defaultCommandId = c.Int(nullable: false, identity: true),
                        command = c.String(),
                    })
                .PrimaryKey(t => t.defaultCommandId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DefaultCommands");
        }
    }
}
