namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InternalTasks", "dueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InternalTasks", "dueDate", c => c.String());
        }
    }
}
