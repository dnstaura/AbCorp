namespace testuser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoDeDatoEstado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Estado", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserViews", "Estado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserViews", "Estado", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Estado", c => c.String());
        }
    }
}
