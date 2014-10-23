namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLinkTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinkTypes",
                c => new
                    {
                        linkid = c.Int(nullable: false, identity: true),
                        link = c.String(),
                        recurses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.linkid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LinkTypes");
        }
    }
}
