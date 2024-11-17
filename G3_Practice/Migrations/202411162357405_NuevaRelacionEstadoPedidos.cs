namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevaRelacionEstadoPedidos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadoPedidoes",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            CreateIndex("dbo.Pedidos", "EstadoId");
            AddForeignKey("dbo.Pedidos", "EstadoId", "dbo.EstadoPedidoes", "EstadoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidos", "EstadoId", "dbo.EstadoPedidoes");
            DropIndex("dbo.Pedidos", new[] { "EstadoId" });
            DropTable("dbo.EstadoPedidoes");
        }
    }
}
