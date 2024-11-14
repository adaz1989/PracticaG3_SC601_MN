namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fuck_ALL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreferenciaAlimenticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuariosPreferencias",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        PreferenciaAlimenticiaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.PreferenciaAlimenticiaId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.PreferenciaAlimenticias", t => t.PreferenciaAlimenticiaId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.PreferenciaAlimenticiaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosPreferencias", "PreferenciaAlimenticiaId", "dbo.PreferenciaAlimenticias");
            DropForeignKey("dbo.UsuariosPreferencias", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsuariosPreferencias", new[] { "PreferenciaAlimenticiaId" });
            DropIndex("dbo.UsuariosPreferencias", new[] { "ApplicationUserId" });
            DropTable("dbo.UsuariosPreferencias");
            DropTable("dbo.PreferenciaAlimenticias");
        }
    }
}
