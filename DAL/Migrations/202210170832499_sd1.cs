namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Buy_CountProduct", name: "counProductInAnbar_id", newName: "counProductInAnbarB_id");
            RenameIndex(table: "dbo.Buy_CountProduct", name: "IX_counProductInAnbar_id", newName: "IX_counProductInAnbarB_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Buy_CountProduct", name: "IX_counProductInAnbarB_id", newName: "IX_counProductInAnbar_id");
            RenameColumn(table: "dbo.Buy_CountProduct", name: "counProductInAnbarB_id", newName: "counProductInAnbar_id");
        }
    }
}
