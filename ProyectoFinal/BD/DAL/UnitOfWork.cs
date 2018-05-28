using ProyectoFinal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.BD.DAL
{
    public class UnitOfWork
    {
        private ClubCineContext context = new ClubCineContext();
        private RepositorioAlquiler alquiler;
        private RepositorioAsiento asiento;
        private RepositorioPelicula pelicula;
        private RepositorioProveedor proveedor;
        private RepositorioReserva reserva;
        private RepositorioSala sala;
        private RepositorioUsuario usuario;


        public RepositorioAlquiler RepositorioAlquiler
        {
            get
            {
                if (this.alquiler == null)
                {
                    this.alquiler = new RepositorioAlquiler(context);
                }
                return alquiler;
            }
        }

        public RepositorioAsiento RepositorioAsiento
        {
            get
            {
                if (this.asiento == null)
                {
                    this.asiento = new RepositorioAsiento(context);
                }
                return asiento;
            }
        }

        public RepositorioPelicula RepositorioPelicula
        {
            get
            {
                if (this.pelicula == null)
                {
                    this.pelicula = new RepositorioPelicula(context);
                }
                return pelicula;
            }
        }

        public RepositorioProveedor RepositorioProveedor
        {
            get
            {
                if (this.proveedor == null)
                {
                    this.proveedor = new RepositorioProveedor(context);
                }
                return proveedor;
            }
        }

        public RepositorioReserva RepositorioReserva
        {
            get
            {
                if (this.reserva == null)
                {
                    this.reserva = new RepositorioReserva(context);
                }
                return reserva;
            }
        }

        public RepositorioSala RepositorioSala
        {
            get
            {
                if (this.sala == null)
                {
                    this.sala = new RepositorioSala(context);
                }
                return sala;
            }
        }

        public RepositorioUsuario RepositorioUsuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new RepositorioUsuario(context);
                }
                return usuario;
            }
        }

    }
}
