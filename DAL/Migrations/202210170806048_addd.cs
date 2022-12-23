namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buy_CountProduct",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        count = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        anbarname = c.String(),
                        Marjo = c.Boolean(nullable: false),
                        MasolMarjo = c.String(),
                        DateMarjo = c.String(),
                        SabtAnbar = c.Boolean(nullable: false),
                        MasolSabt = c.String(),
                        DateSabt = c.String(),
                        buyC_id = c.Int(),
                        counProductInAnbar_id = c.Int(),
                        productB_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Buys", t => t.buyC_id)
                .ForeignKey("dbo.CounProductInAnbars", t => t.counProductInAnbar_id)
                .ForeignKey("dbo.Products", t => t.productB_id)
                .Index(t => t.buyC_id)
                .Index(t => t.counProductInAnbar_id)
                .Index(t => t.productB_id);
            
            CreateTable(
                "dbo.Buys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BuyNumber = c.String(),
                        Type = c.String(),
                        Title = c.String(),
                        Tozih = c.String(),
                        FeepaidB = c.Double(nullable: false),
                        TotalCostB = c.Double(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        IsCheckOut = c.Boolean(nullable: false),
                        CheckOutDate = c.DateTime(),
                        DeleteStatus = c.Boolean(nullable: false),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
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
            DropForeignKey("dbo.Buy_CountProduct", "productB_id", "dbo.Products");
            DropForeignKey("dbo.Buy_CountProduct", "counProductInAnbar_id", "dbo.CounProductInAnbars");
            DropForeignKey("dbo.Buys", "User_id", "dbo.Users");
            DropForeignKey("dbo.BuyProducts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.BuyProducts", "Buy_id", "dbo.Buys");
            DropForeignKey("dbo.Buy_CountProduct", "buyC_id", "dbo.Buys");
            DropIndex("dbo.BuyProducts", new[] { "Product_id" });
            DropIndex("dbo.BuyProducts", new[] { "Buy_id" });
            DropIndex("dbo.Buys", new[] { "User_id" });
            DropIndex("dbo.Buy_CountProduct", new[] { "productB_id" });
            DropIndex("dbo.Buy_CountProduct", new[] { "counProductInAnbar_id" });
            DropIndex("dbo.Buy_CountProduct", new[] { "buyC_id" });
            DropTable("dbo.BuyProducts");
            DropTable("dbo.Buys");
            DropTable("dbo.Buy_CountProduct");
        }
    }
}
