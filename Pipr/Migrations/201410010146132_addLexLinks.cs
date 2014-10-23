namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLexLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LexLinks",
                c => new
                    {
                        synset1id = c.Int(nullable: false),
                        word1id = c.Int(nullable: false),
                        synset2id = c.Int(nullable: false),
                        word2id = c.Int(nullable: false),
                        linkid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.synset1id, t.word1id, t.synset2id, t.word2id, t.linkid });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LexLinks");
        }
    }
}
