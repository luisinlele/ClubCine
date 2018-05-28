using ProyectoFinal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.BD.DAL
{
    public class ClubCineContext : DbContext
    {
        public ClubCineContext() : base("ClubCineEntities") { }

        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
}
}
