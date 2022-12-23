namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class da : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "Marjoei", c => c.Boolean(nullable: false));
            AddColumn("dbo.CountProducts", "SabtKhoroj", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounProductInAnbars", "Kharab", c => c.Boolean(nullable: false));
            DropColumn("dbo.CounProductInAnbars", "Marjoei");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CounProductInAnbars", "Marjoei", c => c.Boolean(nullable: false));
            DropColumn("dbo.CounProductInAnbars", "Kharab");
            DropColumn("dbo.CountProducts", "SabtKhoroj");
            DropColumn("dbo.CountProducts", "Marjoei");
        }
    }
}
