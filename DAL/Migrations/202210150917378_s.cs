namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "anbarname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "anbarname");
        }
    }
}
