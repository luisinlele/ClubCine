namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alquileres", "HabilitadoAlquiler", c => c.Boolean(nullable: false));
            AddColumn("dbo.Peliculas", "HabilitadoPelicula", c => c.Boolean(nullable: false));
            AddColumn("dbo.Proveedores", "HabilitadoProveedor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reservas", "HabilitadoReserva", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuarios", "HabilitadoUsuario", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "HabilitadoUsuario");
            DropColumn("dbo.Reservas", "HabilitadoReserva");
            DropColumn("dbo.Proveedores", "HabilitadoProveedor");
            DropColumn("dbo.Peliculas", "HabilitadoPelicula");
            DropColumn("dbo.Alquileres", "HabilitadoAlquiler");
        }
    }
}
