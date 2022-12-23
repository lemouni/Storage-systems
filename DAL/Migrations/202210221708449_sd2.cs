namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SellDaryafts", "Buy_id", "dbo.Buys");
            DropIndex("dbo.SellDaryafts", new[] { "Buy_id" });
            DropColumn("dbo.SellDaryafts", "Buy_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SellDaryafts", "Buy_id", c => c.Int());
            CreateIndex("dbo.SellDaryafts", "Buy_id");
            AddForeignKey("dbo.SellDaryafts", "Buy_id", "dbo.Buys", "id");
        }
    }
}
