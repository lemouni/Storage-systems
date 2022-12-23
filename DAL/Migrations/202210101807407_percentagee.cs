namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class percentagee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CountProducts", "Percentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CountProducts", "Percentage", c => c.Int(nullable: false));
        }
    }
}
