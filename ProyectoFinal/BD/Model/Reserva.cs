using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Reservas")]
    public class Reserva
    {
        public Reserva()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        [Key]
        public int ReservaId { get; set; }
        public String FechaReserva { get; set; }
        public String HoraReserva { get; set; }
        public int PeliculaPrecioReserva { get; set; }
        public bool HabilitadoReserva { get; set; }

        public int SalaIdReserva { get; set; }
        public virtual Sala Salas { get; set; }

        public int UsuarioIdReserva { get; set; }
        public virtual Usuario Usuarios { get; set; }
        public virtual ICollection<Pelicula> Peliculas { get; set; }

    }
}
