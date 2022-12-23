namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReminderUsers", "Reminder_id", "dbo.Reminders");
            DropForeignKey("dbo.ReminderUsers", "User_id", "dbo.Users");
            DropIndex("dbo.ReminderUsers", new[] { "Reminder_id" });
            DropIndex("dbo.ReminderUsers", new[] { "User_id" });
            AddColumn("dbo.Reminders", "Users_id", c => c.Int());
            CreateIndex("dbo.Reminders", "Users_id");
            AddForeignKey("dbo.Reminders", "Users_id", "dbo.Users", "id");
            DropTable("dbo.ReminderUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReminderUsers",
                c => new
                    {
                        Reminder_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reminder_id, t.User_id });
            
            DropForeignKey("dbo.Reminders", "Users_id", "dbo.Users");
            DropIndex("dbo.Reminders", new[] { "Users_id" });
            DropColumn("dbo.Reminders", "Users_id");
            CreateIndex("dbo.ReminderUsers", "User_id");
            CreateIndex("dbo.ReminderUsers", "Reminder_id");
            AddForeignKey("dbo.ReminderUsers", "User_id", "dbo.Users", "id", cascadeDelete: true);
            AddForeignKey("dbo.ReminderUsers", "Reminder_id", "dbo.Reminders", "id", cascadeDelete: true);
        }
    }
}
