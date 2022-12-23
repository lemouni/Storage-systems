namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdaw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "MasolMarjo", c => c.String());
            AddColumn("dbo.CountProducts", "DateMarjo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "DateMarjo");
            DropColumn("dbo.CountProducts", "MasolMarjo");
        }
    }
}
