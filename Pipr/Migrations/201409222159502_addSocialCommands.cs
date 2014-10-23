namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSocialCommands : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SocialCommands",
                c => new
                    {
                        socialCommandId = c.Int(nullable: false, identity: true),
                        command = c.String(),
                        response = c.String(),
                    })
                .PrimaryKey(t => t.socialCommandId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SocialCommands");
        }
    }
}
