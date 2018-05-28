using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Asientos")]
    public class Asiento
    {
        public Asiento()
        {

        }

        [Key]
        public int AsientoId { get; set; }
        public string NumeroAsiento { get; set; }
        public bool OcupadoAsiento { get; set; }

        public int SalaIdAsiento { get; set; }
        public virtual Sala Salas { get; set; }

    }
}
