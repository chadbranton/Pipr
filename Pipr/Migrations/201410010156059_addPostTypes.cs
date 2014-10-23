namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        pos = c.Int(nullable: false),
                        posname = c.String(),
                    })
                .PrimaryKey(t => t.pos);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostTypes");
        }
    }
}
