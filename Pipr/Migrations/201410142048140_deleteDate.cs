namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InternalTasks", "dueDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InternalTasks", "dueDate", c => c.DateTime(nullable: false));
        }
    }
}
