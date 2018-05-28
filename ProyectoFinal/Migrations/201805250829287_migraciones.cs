namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migraciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alquileres",
                c => new
                    {
                        AlquilerId = c.Int(nullable: false, identity: true),
                        FechaAlquiler = c.DateTime(nullable: false),
                        FechaMaxAlquiler = c.DateTime(nullable: false),
                        FechaDevolucionAlquiler = c.DateTime(nullable: false),
                        PrecioAlquiler = c.Int(nullable: false),
                        UsuarioIdReserva = c.Int(nullable: false),
                        Usuarios_UsuarioId = c.Long(),
                    })
                .PrimaryKey(t => t.AlquilerId)
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_UsuarioId)
                .Index(t => t.Usuarios_UsuarioId);
            
            CreateTable(
                "dbo.Peliculas",
                c => new
                    {
                        PeliculaId = c.Int(nullable: false, identity: true),
                        NombrePelicula = c.String(),
                        GeneroPelicula = c.String(),
                        AñoPelicula = c.String(),
                        CartelPelicula = c.String(),
                        PrecioPelicula = c.Double(nullable: false),
                        TrailerPelicula = c.String(),
                        ProveedorIdPelicula = c.Int(nullable: false),
                        Proveedores_ProveedorId = c.Int(),
                    })
                .PrimaryKey(t => t.PeliculaId)
                .ForeignKey("dbo.Proveedores", t => t.Proveedores_ProveedorId)
                .Index(t => t.Proveedores_ProveedorId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        NombreProveedor = c.String(),
                        CorreoContactoProveedor = c.String(),
                        TelefonoProveedor = c.String(),
                        DireccionProveedor = c.String(),
                        PaisProveedor = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ReservaId = c.Int(nullable: false, identity: true),
                        FechaReserva = c.String(),
                        HoraReserva = c.String(),
                        PeliculaPrecioReserva = c.Int(nullable: false),
                        SalaIdReserva = c.Int(nullable: false),
                        UsuarioIdReserva = c.Int(nullable: false),
                        Salas_SalaId = c.Int(),
                        Usuarios_UsuarioId = c.Long(),
                    })
                .PrimaryKey(t => t.ReservaId)
                .ForeignKey("dbo.Salas", t => t.Salas_SalaId)
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_UsuarioId)
                .Index(t => t.Salas_SalaId)
                .Index(t => t.Usuarios_UsuarioId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        SalaId = c.Int(nullable: false, identity: true),
                        NumeroSala = c.String(),
                    })
                .PrimaryKey(t => t.SalaId);
            
            CreateTable(
                "dbo.Asientos",
                c => new
                    {
                        AsientoId = c.Int(nullable: false, identity: true),
                        NumeroAsiento = c.String(),
                        OcupadoAsiento = c.Boolean(nullable: false),
                        SalaIdAsiento = c.Int(nullable: false),
                        Salas_SalaId = c.Int(),
                    })
                .PrimaryKey(t => t.AsientoId)
                .ForeignKey("dbo.Salas", t => t.Salas_SalaId)
                .Index(t => t.Salas_SalaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Long(nullable: false, identity: true),
                        NombreUsuario = c.String(),
                        ApellidosUsuario = c.String(),
                        EmailUsuario = c.String(),
                        ContraseñaUsuario = c.String(),
                        TelefonoUsuario = c.String(),
                        RolUsuario = c.String(),
                        FechaCreacionUsuario = c.DateTime(nullable: false),
                        PerfilUsuario = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.PeliculaAlquilers",
                c => new
                    {
                        Pelicula_PeliculaId = c.Int(nullable: false),
                        Alquiler_AlquilerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pelicula_PeliculaId, t.Alquiler_AlquilerId })
                .ForeignKey("dbo.Peliculas", t => t.Pelicula_PeliculaId, cascadeDelete: true)
                .ForeignKey("dbo.Alquileres", t => t.Alquiler_AlquilerId, cascadeDelete: true)
                .Index(t => t.Pelicula_PeliculaId)
                .Index(t => t.Alquiler_AlquilerId);
            
            CreateTable(
                "dbo.ReservaPeliculas",
                c => new
                    {
                        Reserva_ReservaId = c.Int(nullable: false),
                        Pelicula_PeliculaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reserva_ReservaId, t.Pelicula_PeliculaId })
                .ForeignKey("dbo.Reservas", t => t.Reserva_ReservaId, cascadeDelete: true)
                .ForeignKey("dbo.Peliculas", t => t.Pelicula_PeliculaId, cascadeDelete: true)
                .Index(t => t.Reserva_ReservaId)
                .Index(t => t.Pelicula_PeliculaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Usuarios_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Alquileres", "Usuarios_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Reservas", "Salas_SalaId", "dbo.Salas");
            DropForeignKey("dbo.Asientos", "Salas_SalaId", "dbo.Salas");
            DropForeignKey("dbo.ReservaPeliculas", "Pelicula_PeliculaId", "dbo.Peliculas");
            DropForeignKey("dbo.ReservaPeliculas", "Reserva_ReservaId", "dbo.Reservas");
            DropForeignKey("dbo.Peliculas", "Proveedores_ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.PeliculaAlquilers", "Alquiler_AlquilerId", "dbo.Alquileres");
            DropForeignKey("dbo.PeliculaAlquilers", "Pelicula_PeliculaId", "dbo.Peliculas");
            DropIndex("dbo.ReservaPeliculas", new[] { "Pelicula_PeliculaId" });
            DropIndex("dbo.ReservaPeliculas", new[] { "Reserva_ReservaId" });
            DropIndex("dbo.PeliculaAlquilers", new[] { "Alquiler_AlquilerId" });
            DropIndex("dbo.PeliculaAlquilers", new[] { "Pelicula_PeliculaId" });
            DropIndex("dbo.Asientos", new[] { "Salas_SalaId" });
            DropIndex("dbo.Reservas", new[] { "Usuarios_UsuarioId" });
            DropIndex("dbo.Reservas", new[] { "Salas_SalaId" });
            DropIndex("dbo.Peliculas", new[] { "Proveedores_ProveedorId" });
            DropIndex("dbo.Alquileres", new[] { "Usuarios_UsuarioId" });
            DropTable("dbo.ReservaPeliculas");
            DropTable("dbo.PeliculaAlquilers");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Asientos");
            DropTable("dbo.Salas");
            DropTable("dbo.Reservas");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Peliculas");
            DropTable("dbo.Alquileres");
        }
    }
}
