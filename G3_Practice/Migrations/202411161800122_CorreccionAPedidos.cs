namespace G3_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionAPedidos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedidos", "DireccionEntrega", c => c.String());
            AlterColumn("dbo.Pedidos", "MetodoPago", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedidos", "MetodoPago", c => c.Int(nullable: false));
            AlterColumn("dbo.Pedidos", "DireccionEntrega", c => c.Int(nullable: false));
        }
    }
}
