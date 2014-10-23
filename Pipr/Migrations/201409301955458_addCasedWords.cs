namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCasedWords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CasedWords",
                c => new
                    {
                        casedWordId = c.Int(nullable: false, identity: true),
                        wordid = c.Int(nullable: false),
                        cased = c.String(),
                    })
                .PrimaryKey(t => t.casedWordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CasedWords");
        }
    }
}
