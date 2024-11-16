namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevosDatosAlCarrito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarritoDetalles", "ProductoName", c => c.String());
            AddColumn("dbo.CarritoDetalles", "Price", c => c.Single());
            AddColumn("dbo.CarritoDetalles", "stock", c => c.Int(nullable: false));
            AddColumn("dbo.CarritoDetalles", "imagen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarritoDetalles", "imagen");
            DropColumn("dbo.CarritoDetalles", "stock");
            DropColumn("dbo.CarritoDetalles", "Price");
            DropColumn("dbo.CarritoDetalles", "ProductoName");
        }
    }
}
