using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Proveedores")]
    public class Proveedor
    {
        public Proveedor()
        {
            Peliculas = new Collection<Pelicula>();
        }

        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string CorreoContactoProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public string PaisProveedor { get; set; }
        public bool HabilitadoProveedor { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
