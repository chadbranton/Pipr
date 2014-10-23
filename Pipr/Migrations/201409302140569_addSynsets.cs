namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSynsets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Synsets",
                c => new
                    {
                        synsetid = c.Int(nullable: false, identity: true),
                        pos = c.Int(nullable: false),
                        lexdomainid = c.Int(nullable: false),
                        definition = c.String(),
                    })
                .PrimaryKey(t => t.synsetid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Synsets");
        }
    }
}
