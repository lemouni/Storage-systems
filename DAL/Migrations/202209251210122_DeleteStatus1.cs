namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatus1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reminders", "DeleteStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Reminders", "IsDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reminders", "IsDone", c => c.Boolean());
            DropColumn("dbo.Reminders", "DeleteStatus");
        }
    }
}
