namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDueDateToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InternalTasks", "dueDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InternalTasks", "dueDate");
        }
    }
}
