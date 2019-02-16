namespace testuser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Module = c.String(),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Usuario_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "Usuario_Id" });
            DropTable("dbo.Notifications");
        }
    }
}
