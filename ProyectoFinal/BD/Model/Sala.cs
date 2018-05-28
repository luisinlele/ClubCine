using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Salas")]
    public class Sala
    {
        public Sala()
        {
            Asientos = new Collection<Asiento>();
            Reservas = new Collection<Reserva>();
        }

        [Key]
        public int SalaId { get; set; }
        public string NumeroSala { get; set; }

        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
