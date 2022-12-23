namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdaw2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Buys", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Buys", "CheckOutDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Buys", "CheckOutDate", c => c.DateTime());
            AlterColumn("dbo.Buys", "RegDate", c => c.DateTime(nullable: false));
        }
    }
}
