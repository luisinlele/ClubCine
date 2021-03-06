﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Model
{
    [Table("Alquileres")]
    public class Alquiler
    {
        public Alquiler()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        [Key]
        public int AlquilerId { get; set; }


        public String FechaAlquiler { get; set; }

        public String FechaMaxAlquiler { get; set; }

        
        public string FechaDevolucionAlquiler { get; set; }

        public int PrecioAlquiler { get; set; }

        public bool HabilitadoAlquiler { get; set; }

        public int UsuarioIdReserva { get; set; }
        public virtual Usuario Usuarios { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }

    }
}
