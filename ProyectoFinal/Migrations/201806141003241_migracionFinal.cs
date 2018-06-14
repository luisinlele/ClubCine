namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracionFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alquileres", "FechaAlquiler", c => c.String());
            AlterColumn("dbo.Alquileres", "FechaMaxAlquiler", c => c.String());
            AlterColumn("dbo.Alquileres", "FechaDevolucionAlquiler", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alquileres", "FechaDevolucionAlquiler", c => c.DateTime());
            AlterColumn("dbo.Alquileres", "FechaMaxAlquiler", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Alquileres", "FechaAlquiler", c => c.DateTime(nullable: false));
        }
    }
}
