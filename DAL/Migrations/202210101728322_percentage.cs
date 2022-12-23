namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class percentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "Percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "Percentage");
        }
    }
}
