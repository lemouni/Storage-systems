namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatus2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "DeleteStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "DeleteStatus");
        }
    }
}
