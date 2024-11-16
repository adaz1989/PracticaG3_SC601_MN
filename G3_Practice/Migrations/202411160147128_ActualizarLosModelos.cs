namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarLosModelos : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Productos", "Cantidad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productos", "Cantidad", c => c.Int(nullable: false));
        }
    }
}
