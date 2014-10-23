namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSenses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Senses",
                c => new
                    {
                        wordid = c.Int(nullable: false),
                        synsetid = c.Int(nullable: false),
                        casedwordid = c.Int(nullable: false),
                        senseid = c.Int(nullable: false),
                        sensenum = c.Int(nullable: false),
                        lexid = c.Int(nullable: false),
                        tagcount = c.Int(nullable: false),
                        sensekey = c.String(),
                    })
                .PrimaryKey(t => new { t.wordid, t.synsetid });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Senses");
        }
    }
}
