namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ojala_Funcione : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoDetalles",
                c => new
                    {
                        CarritoDetalleId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        ProuctoId = c.Int(nullable: false),
                        Cantiad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarritoDetalleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProuctoId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.ProuctoId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        FechaPedido = c.DateTime(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.PedidoDetalles",
                c => new
                    {
                        PedidoDetalleId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Single(nullable: false),
                        SubTotal = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoDetalleId)
                .ForeignKey("dbo.Pedidos", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        ProductoName = c.String(),
                        Price = c.Single(),
                        stock = c.Int(nullable: false),
                        imagen = c.String(),
                    })
                .PrimaryKey(t => t.ProductoId);
            
            CreateTable(
                "dbo.CategoriaxProductoes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductoId, t.CategoriaId })
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        TipoCategoria = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                        MetodoPago = c.String(),
                        TotalVenta = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Pedidos", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoDetalles", "ProuctoId", "dbo.Productos");
            DropForeignKey("dbo.CarritoDetalles", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ventas", "PedidoId", "dbo.Pedidos");
            DropForeignKey("dbo.PedidoDetalles", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.CategoriaxProductoes", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.CategoriaxProductoes", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.PedidoDetalles", "PedidoId", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "UsuarioId", "dbo.AspNetUsers");
            DropIndex("dbo.Ventas", new[] { "PedidoId" });
            DropIndex("dbo.CategoriaxProductoes", new[] { "CategoriaId" });
            DropIndex("dbo.CategoriaxProductoes", new[] { "ProductoId" });
            DropIndex("dbo.PedidoDetalles", new[] { "ProductoId" });
            DropIndex("dbo.PedidoDetalles", new[] { "PedidoId" });
            DropIndex("dbo.Pedidos", new[] { "UsuarioId" });
            DropIndex("dbo.CarritoDetalles", new[] { "ProuctoId" });
            DropIndex("dbo.CarritoDetalles", new[] { "UsuarioId" });
            DropTable("dbo.Ventas");
            DropTable("dbo.Categorias");
            DropTable("dbo.CategoriaxProductoes");
            DropTable("dbo.Productos");
            DropTable("dbo.PedidoDetalles");
            DropTable("dbo.Pedidos");
            DropTable("dbo.CarritoDetalles");
        }
    }
}
