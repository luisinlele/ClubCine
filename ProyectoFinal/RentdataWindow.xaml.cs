using ProyectoFinal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFinal
{
    /// <summary>
    /// Lógica de interacción para RentdataWindow.xaml
    /// </summary>
    public partial class RentdataWindow : Window
    {
        Usuario usuario;
        List<Pelicula> pelicula;

        public RentdataWindow(Usuario usuario, List<Pelicula> aReservar)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.pelicula = aReservar;
            datagrid_ToRent.ItemsSource = aReservar;
        }
    }
}
