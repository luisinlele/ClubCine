using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Peliculas")]
    public class Pelicula
    {
        public Pelicula()
        {
            Alquileres = new HashSet<Alquiler>();
            Reservas = new HashSet<Reserva>();
        }
        [Key]
        public int PeliculaId { get; set; }

        public string NombrePelicula { get; set; }

        public string GeneroPelicula { get; set; }

        public string AñoPelicula { get; set; }

        public string CartelPelicula { get; set; }

        public double PrecioPelicula { get; set; }

        public string TrailerPelicula { get; set; }

        public bool HabilitadoPelicula { get; set; }

        public int ProveedorIdPelicula { get; set; }
        public virtual Proveedor Proveedores { get; set; }

        public virtual ICollection<Alquiler> Alquileres { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }




    }
}
