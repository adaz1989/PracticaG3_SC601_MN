namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarELCarrito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarritoDetalles", "TotalPrice", c => c.Single());
            AddColumn("dbo.CarritoDetalles", "Total", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarritoDetalles", "Total");
            DropColumn("dbo.CarritoDetalles", "TotalPrice");
        }
    }
}
