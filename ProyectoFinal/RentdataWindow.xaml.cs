using ProyectoFinal.BD.DAL;
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
        Pelicula pelicula;
        Alquiler alquiler = new Alquiler();
        UnitOfWork uow = new UnitOfWork();
        

        public RentdataWindow(Usuario usuario, Pelicula aReservar)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.pelicula = aReservar;
            datagrid_ToRent.ItemsSource = uow.RepositorioPelicula.ObtenerVarios(c => c.NombrePelicula.Equals(pelicula.NombrePelicula));
            CompletarData();
            LoadPoster();
        }

        public void CompletarData()
        {
            textbox_NameUser.Text = usuario.NombreUsuario;
            textbox_SurnameUser.Text = usuario.ApellidosUsuario;
            textbox_PhoneUser.Text = usuario.TelefonoUsuario;
        }

        private void button_Rent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("¿Quieres hacer el alquiler? ", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:

                    DateTime data = new DateTime();
                    data = DateTime.Today;
                    alquiler.FechaAlquiler = data;
                    alquiler.FechaMaxAlquiler = data.AddDays(7);
                    alquiler.PrecioAlquiler = Convert.ToInt32(pelicula.PrecioPelicula);
                    alquiler.UsuarioIdReserva = Convert.ToInt32(usuario.UsuarioId);
                    uow.RepositorioAlquiler.Crear(alquiler);
                    MessageBoxResult rent = MessageBox.Show("Alquiler hecho correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("¿Seguro que quieres calcelar? ", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
            }
        }

        public void LoadPoster()
        {
            try
            {
                BitmapImage bit2 = new BitmapImage();
                bit2.BeginInit();
                bit2.UriSource = new Uri(pelicula.CartelPelicula);
                bit2.EndInit();
                image_FilmPoster.Source = bit2;
                image_FilmPoster.Width = 115;
                image_FilmPoster.Height = 134;
            }
            catch (Exception)
            {
            }
        }


    }
}
