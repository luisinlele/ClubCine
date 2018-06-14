namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alquileres", "FechaDevolucionAlquiler", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alquileres", "FechaDevolucionAlquiler", c => c.DateTime(nullable: false));
        }
    }
}
