namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarDireccionEntregaMetodoPagoAPedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidos", "DireccionEntrega", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Pedidos", "MetodoPago", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedidos", "MetodoPago");
            DropColumn("dbo.Pedidos", "DireccionEntrega");
        }
    }
}
