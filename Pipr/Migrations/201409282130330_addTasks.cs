namespace Pipr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskLists",
                c => new
                    {
                        taskListId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.taskListId);
            
            CreateTable(
                "dbo.InternalTasks",
                c => new
                    {
                        internalTaskId = c.Int(nullable: false, identity: true),
                        taskName = c.String(),
                        complete = c.Boolean(nullable: false),
                        TaskList_taskListId = c.Int(),
                    })
                .PrimaryKey(t => t.internalTaskId)
                .ForeignKey("dbo.TaskLists", t => t.TaskList_taskListId)
                .Index(t => t.TaskList_taskListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InternalTasks", "TaskList_taskListId", "dbo.TaskLists");
            DropIndex("dbo.InternalTasks", new[] { "TaskList_taskListId" });
            DropTable("dbo.InternalTasks");
            DropTable("dbo.TaskLists");
        }
    }
}
