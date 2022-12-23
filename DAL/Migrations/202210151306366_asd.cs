namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "Datekhoroj", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "Datekhoroj");
        }
    }
}
