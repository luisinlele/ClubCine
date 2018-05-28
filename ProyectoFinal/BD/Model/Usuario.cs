using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Usuarios")]
    public class Usuario
    {
        public Usuario()
        {
            Alquileres = new HashSet<Alquiler>();
            Reservas = new HashSet<Reserva>();
        }

        //FALTA Validate Model Data Using DataAnnotations Attributes
        [Key]
        public long UsuarioId { get; set; }

        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public string RolUsuario { get; set; } //Normal o admin
        public DateTime FechaCreacionUsuario { get; set; }
        public string PerfilUsuario { get; set; }

        public virtual ICollection<Alquiler> Alquileres { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
