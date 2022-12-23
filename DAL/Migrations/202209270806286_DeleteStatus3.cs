namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatus3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityCategories", "DeleteStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityCategories", "DeleteStatus");
        }
    }
}
