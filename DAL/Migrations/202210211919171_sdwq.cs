namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyPardakhts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Pardakhti = c.Double(nullable: false),
                        DatePardakht = c.String(),
                        PardakhtShod = c.Boolean(nullable: false),
                        MasolPardakhtShod = c.String(),
                        DatePardakhtShod = c.String(),
                        Bargasht = c.Boolean(nullable: false),
                        MasolBargasht = c.String(),
                        DateBargasht = c.String(),
                        Buy_id = c.Int(),
                        HesabBank_id = c.Int(),
                        NoePardakht_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Buys", t => t.Buy_id)
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id)
                .ForeignKey("dbo.NoePardakhts", t => t.NoePardakht_id)
                .Index(t => t.Buy_id)
                .Index(t => t.HesabBank_id)
                .Index(t => t.NoePardakht_id);
            
            CreateTable(
                "dbo.HesabBanks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Sheba = c.String(),
                        ShomareHesab = c.String(),
                        Stock = c.Double(nullable: false),
                        DeleteStatus = c.Boolean(nullable: false),
                        CategoriNoe_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CategoriNoes", t => t.CategoriNoe_id)
                .Index(t => t.CategoriNoe_id);
            
            CreateTable(
                "dbo.NoePardakhts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        MoJodi = c.Double(nullable: false),
                        CategoriNoe_id = c.Int(),
                        HesabBank_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CategoriNoes", t => t.CategoriNoe_id)
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id)
                .Index(t => t.CategoriNoe_id)
                .Index(t => t.HesabBank_id);
            
            CreateTable(
                "dbo.CategoriNoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SellDaryafts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Daryafti = c.Double(nullable: false),
                        DateDaryafti = c.String(),
                        DaryaftShod = c.Boolean(nullable: false),
                        MasolDaryaftShod = c.String(),
                        DateDaryaftShod = c.String(),
                        Bargasht = c.Boolean(nullable: false),
                        MasolBargasht = c.String(),
                        DateBargasht = c.String(),
                        Buy_id = c.Int(),
                        HesabBank_id = c.Int(),
                        NoePardakht_id = c.Int(),
                        Invoice_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Buys", t => t.Buy_id)
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id)
                .ForeignKey("dbo.NoePardakhts", t => t.NoePardakht_id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_id)
                .Index(t => t.Buy_id)
                .Index(t => t.HesabBank_id)
                .Index(t => t.NoePardakht_id)
                .Index(t => t.Invoice_id);
            
            CreateTable(
                "dbo.HesabBankBuys",
                c => new
                    {
                        HesabBank_id = c.Int(nullable: false),
                        Buy_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HesabBank_id, t.Buy_id })
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id, cascadeDelete: true)
                .ForeignKey("dbo.Buys", t => t.Buy_id, cascadeDelete: true)
                .Index(t => t.HesabBank_id)
                .Index(t => t.Buy_id);
            
            CreateTable(
                "dbo.HesabBankInvoices",
                c => new
                    {
                        HesabBank_id = c.Int(nullable: false),
                        Invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HesabBank_id, t.Invoice_id })
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.Invoice_id, cascadeDelete: true)
                .Index(t => t.HesabBank_id)
                .Index(t => t.Invoice_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SellDaryafts", "Invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.SellDaryafts", "NoePardakht_id", "dbo.NoePardakhts");
            DropForeignKey("dbo.SellDaryafts", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.SellDaryafts", "Buy_id", "dbo.Buys");
            DropForeignKey("dbo.NoePardakhts", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.NoePardakhts", "CategoriNoe_id", "dbo.CategoriNoes");
            DropForeignKey("dbo.HesabBanks", "CategoriNoe_id", "dbo.CategoriNoes");
            DropForeignKey("dbo.BuyPardakhts", "NoePardakht_id", "dbo.NoePardakhts");
            DropForeignKey("dbo.HesabBankInvoices", "Invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.HesabBankInvoices", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.HesabBankBuys", "Buy_id", "dbo.Buys");
            DropForeignKey("dbo.HesabBankBuys", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.BuyPardakhts", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.BuyPardakhts", "Buy_id", "dbo.Buys");
            DropIndex("dbo.HesabBankInvoices", new[] { "Invoice_id" });
            DropIndex("dbo.HesabBankInvoices", new[] { "HesabBank_id" });
            DropIndex("dbo.HesabBankBuys", new[] { "Buy_id" });
            DropIndex("dbo.HesabBankBuys", new[] { "HesabBank_id" });
            DropIndex("dbo.SellDaryafts", new[] { "Invoice_id" });
            DropIndex("dbo.SellDaryafts", new[] { "NoePardakht_id" });
            DropIndex("dbo.SellDaryafts", new[] { "HesabBank_id" });
            DropIndex("dbo.SellDaryafts", new[] { "Buy_id" });
            DropIndex("dbo.NoePardakhts", new[] { "HesabBank_id" });
            DropIndex("dbo.NoePardakhts", new[] { "CategoriNoe_id" });
            DropIndex("dbo.HesabBanks", new[] { "CategoriNoe_id" });
            DropIndex("dbo.BuyPardakhts", new[] { "NoePardakht_id" });
            DropIndex("dbo.BuyPardakhts", new[] { "HesabBank_id" });
            DropIndex("dbo.BuyPardakhts", new[] { "Buy_id" });
            DropTable("dbo.HesabBankInvoices");
            DropTable("dbo.HesabBankBuys");
            DropTable("dbo.SellDaryafts");
            DropTable("dbo.CategoriNoes");
            DropTable("dbo.NoePardakhts");
            DropTable("dbo.HesabBanks");
            DropTable("dbo.BuyPardakhts");
        }
    }
}
