namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addShellCommands : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShellCommands",
                c => new
                    {
                        shellCommandId = c.Int(nullable: false, identity: true),
                        command = c.String(),
                        response = c.String(),
                        location = c.String(),
                    })
                .PrimaryKey(t => t.shellCommandId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShellCommands");
        }
    }
}
