namespace testuser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarTablasView : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserViews", "Role_RoleID", "dbo.RoleViews");
            DropForeignKey("dbo.RoleViews", "UserView_UserID", "dbo.UserViews");
            DropIndex("dbo.RoleViews", new[] { "UserView_UserID" });
            DropIndex("dbo.UserViews", new[] { "Role_RoleID" });
            DropTable("dbo.RoleViews");
            DropTable("dbo.UserViews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserViews",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Estado = c.Boolean(nullable: false),
                        Role_RoleID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.RoleViews",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        UserView_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateIndex("dbo.UserViews", "Role_RoleID");
            CreateIndex("dbo.RoleViews", "UserView_UserID");
            AddForeignKey("dbo.RoleViews", "UserView_UserID", "dbo.UserViews", "UserID");
            AddForeignKey("dbo.UserViews", "Role_RoleID", "dbo.RoleViews", "RoleID");
        }
    }
}
