namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserAccessRoles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Section = c.String(),
                        CanEnter = c.Boolean(nullable: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanUpdate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        UserGroup_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_id)
                .Index(t => t.UserGroup_id);
            
            AddColumn("dbo.Users", "UserGroup_id", c => c.Int());
            CreateIndex("dbo.Users", "UserGroup_id");
            AddForeignKey("dbo.Users", "UserGroup_id", "dbo.UserGroups", "id");
            DropColumn("dbo.Users", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Status", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Users", "UserGroup_id", "dbo.UserGroups");
            DropForeignKey("dbo.UserAccessRoles", "UserGroup_id", "dbo.UserGroups");
            DropIndex("dbo.UserAccessRoles", new[] { "UserGroup_id" });
            DropIndex("dbo.Users", new[] { "UserGroup_id" });
            DropColumn("dbo.Users", "UserGroup_id");
            DropTable("dbo.UserAccessRoles");
            DropTable("dbo.UserGroups");
        }
    }
}
