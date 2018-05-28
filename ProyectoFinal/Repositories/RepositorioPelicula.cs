using ProyectoFinal.BD.DAL;
using ProyectoFinal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVideoclub.DAL;

namespace ProyectoFinal.Repositories
{
    public class RepositorioPelicula : RepositorioGenerico<Pelicula>
    {
        public RepositorioPelicula(ClubCineContext context) : base(context)
        {

        }
    }
}
