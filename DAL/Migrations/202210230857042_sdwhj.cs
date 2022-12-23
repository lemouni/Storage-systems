namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwhj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuyPardakhts", "Tozih", c => c.String());
            AddColumn("dbo.SellDaryafts", "Tozih", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SellDaryafts", "Tozih");
            DropColumn("dbo.BuyPardakhts", "Tozih");
        }
    }
}
