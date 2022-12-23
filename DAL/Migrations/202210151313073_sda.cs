namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sda : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CountProducts", "Datekhoroj", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CountProducts", "Datekhoroj", c => c.DateTime(nullable: false));
        }
    }
}
