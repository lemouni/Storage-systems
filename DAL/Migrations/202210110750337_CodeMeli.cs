namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeMeli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Adress", c => c.String());
            AddColumn("dbo.Customers", "CodePost", c => c.String());
            AddColumn("dbo.Customers", "CodeMeli", c => c.String());
            AddColumn("dbo.Customers", "CodeEghtesadi", c => c.String());
            AddColumn("dbo.Customers", "AccountGroup", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "AccountGroup");
            DropColumn("dbo.Customers", "CodeEghtesadi");
            DropColumn("dbo.Customers", "CodeMeli");
            DropColumn("dbo.Customers", "CodePost");
            DropColumn("dbo.Customers", "Adress");
        }
    }
}
