namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        wordid = c.Int(nullable: false, identity: true),
                        lemma = c.String(),
                    })
                .PrimaryKey(t => t.wordid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Words");
        }
    }
}
