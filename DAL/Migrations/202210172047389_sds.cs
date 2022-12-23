namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyProducts",
                c => new
                    {
                        Buy_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Buy_id, t.Product_id })
                .ForeignKey("dbo.Buys", t => t.Buy_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.Buy_id)
                .Index(t => t.Product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyProducts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.BuyProducts", "Buy_id", "dbo.Buys");
            DropIndex("dbo.BuyProducts", new[] { "Product_id" });
            DropIndex("dbo.BuyProducts", new[] { "Buy_id" });
            DropTable("dbo.BuyProducts");
        }
    }
}
