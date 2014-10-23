namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSemLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SemLinks",
                c => new
                    {
                        linkid = c.Int(nullable: false),
                        synset1id = c.Int(nullable: false),
                        synset2id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.linkid, t.synset1id, t.synset2id });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SemLinks");
        }
    }
}
