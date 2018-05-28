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
    public class RepositorioUsuario : RepositorioGenerico<Usuario>
    {
        public RepositorioUsuario(ClubCineContext context) : base(context)
        {

        }
    }
}
