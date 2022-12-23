namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdaw1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BuyProducts", "Buy_id", "dbo.Buys");
            DropForeignKey("dbo.BuyProducts", "Product_id", "dbo.Products");
            DropIndex("dbo.BuyProducts", new[] { "Buy_id" });
            DropIndex("dbo.BuyProducts", new[] { "Product_id" });
            DropTable("dbo.BuyProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BuyProducts",
                c => new
                    {
                        Buy_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Buy_id, t.Product_id });
            
            CreateIndex("dbo.BuyProducts", "Product_id");
            CreateIndex("dbo.BuyProducts", "Buy_id");
            AddForeignKey("dbo.BuyProducts", "Product_id", "dbo.Products", "id", cascadeDelete: true);
            AddForeignKey("dbo.BuyProducts", "Buy_id", "dbo.Buys", "id", cascadeDelete: true);
        }
    }
}
