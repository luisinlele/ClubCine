﻿using System;
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
using Microsoft.Win32;
using ProyectoFinal.BD.DAL;
using ProyectoFinal.Model;
using ProyectoFinal.Salas;

namespace ProyectoFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Auditorioum Windows
        Auditorium1Window auditorium1;
        Auditorium2Window auditorium2;
        Auditorium3Window auditorium3;
        Auditorium4Window auditorium4;
        Auditorium5Window auditorium5;
        Auditorium6Window auditorium6;

        //Trailer Window
        TrailerWindow trailerWindow;

        //Login Window
        LoginWindow loginWindow;

        //Explorer
        OpenFileDialog explorador = new OpenFileDialog();

        //The Unit Of Work
        UnitOfWork uow = new UnitOfWork();

        //Pelicula
        Pelicula pelicula = new Pelicula();

        //Proveedor
        Proveedor proveedor = new Proveedor();

        Usuario usuario;

        //The rol of the user
        String rol;
       

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            UnloadAll();

            GridFilms.DataContext = pelicula;
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerTodo().ToList();
            LoadComboboxProviderId();
            CleanTextboxPelicula();

            GridProvider.DataContext = proveedor;
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerTodo().ToList();
            CleanTextboxProveedor();

            this.usuario = usuario;

            label_UserEmail.Content = "Usuario: "+ usuario.EmailUsuario;
            label_UserId.Content = "Id: " + usuario.UsuarioId;
            rol = usuario.RolUsuario;
            CheckUser();

            datagrid_Book.ItemsSource = uow.RepositorioReserva.ObtenerTodo().ToList();
        }


        #region Cartelera

        private void button_Loadsits1Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium1 = new Auditorium1Window(usuario);
            auditorium1.Show();
        }
        private void button_Loadsits2Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium2 = new Auditorium2Window();
            auditorium2.Show();
        }

        private void button_Loadsits3artelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium3 = new Auditorium3Window();
            auditorium3.Show();
        }

        private void button_Loadsits4Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium4 = new Auditorium4Window();
            auditorium4.Show();
        }

        private void button_Loadsits5Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium5 = new Auditorium5Window();
            auditorium5.Show();
        }

        private void button_Loadsits6artelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium6 = new Auditorium6Window();
            auditorium6.Show();
        }

        //For when you click on an Film image
        private void filmImage_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerWindow = new TrailerWindow();
            trailerWindow.ShowDialog();
        }




        #endregion Cartelera

        #region Peliculas

        //Save Button of Pelicula
        private void button_SaveFilm_Click(object sender, RoutedEventArgs e)
        {
            pelicula.CartelPelicula = textbox_PosterFilm.Text;
            pelicula.TrailerPelicula = textbox_TrailerFilm.Text;
            uow.RepositorioPelicula.Crear(pelicula);
            MessageBoxResult confirmation = MessageBox.Show("Película añadida Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerTodo().ToList();
            CleanTextboxPelicula();
        }

        //Modify Button of Pelicula
        private void button_ModifyFilm_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioPelicula.Actualizar(pelicula);
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerTodo().ToList();
            CleanTextboxPelicula();
        }

        //Delete Button of Pelicula
        private void button_DeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioPelicula.Eliminar(pelicula);
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerTodo().ToList();
            CleanPelicula();
        }

        //When you click on data of the Pelicula DataGrid
        private void datagrid_Film_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                pelicula = (Pelicula)datagrid_Film.SelectedItem;
                GridFilms.DataContext = pelicula;
            }
            catch (Exception)
            {
            }
        }

        private void button_PosterSelect_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_PosterFilm.Text = explorador.FileName.ToString();
            ShowPreviewPoster(textbox_PosterFilm.Text);
        }


        private void button_TrailerSelect_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "All Video Files(*.MP4;*.3GP;*.WEBM;*.WMV)|*.MP4;*.3GP;*.WEBM;*.WMV|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_TrailerFilm.Text = explorador.FileName.ToString();
            Image_VideoFake.Visibility = Visibility.Hidden;
            MediaElement_Trailer.Visibility = Visibility.Visible;
            ShowPreviewTrailer(textbox_TrailerFilm.Text);
        }

        #endregion Peliculas

        #region Proveedores

        //Save Button of Proveedor
        private void button_SaveProvider_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioProveedor.Crear(proveedor);
            MessageBoxResult confirmation = MessageBox.Show("Proveedor añadido Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerTodo().ToList();
            LoadComboboxProviderId();
            CleanTextboxProveedor();
        }

        //Modify Button of Proveedor
        private void button_ModifyProvider_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioProveedor.Actualizar(proveedor);
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerTodo().ToList();
            CleanTextboxProveedor();
        }

        //Delete Button of Proveedor
        private void button_DeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioProveedor.Eliminar(proveedor);
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerTodo().ToList();
            CleanProveedor();
        }

        //When you click on data of the Proveedor DataGrid
        private void datagrid_Provider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                proveedor = (Proveedor)datagrid_Provider.SelectedItem;
                GridProvider.DataContext = proveedor;
            }
            catch (Exception)
            {
            }
        }

        #endregion Proveedores

        #region General

        //When you close the window, this opens the Login Window
        private void WindowClosed(object sender, EventArgs e)
        {
            loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        #endregion General

        #region Methods

        //Shows a preview of the Film's Poster
        private void ShowPreviewPoster(string ruta)
        {
            try
            {
                BitmapImage bit2 = new BitmapImage();
                bit2.BeginInit();
                bit2.UriSource = new Uri(ruta);
                bit2.EndInit();
                Image_Poster.Source = bit2;
                Image_Poster.Width = 125;
                Image_Poster.Height = 139;
            }
            catch (Exception)
            {
            }

        }

        //Shows a preview of the Film's Trailer
        private void ShowPreviewTrailer(string ruta)
        {
            try
            {
                Uri pathUri = new Uri(ruta);
                MediaElement_Trailer.Source = pathUri;
                MediaElement_Trailer.Width = 156.8;
                MediaElement_Trailer.Height = 121.6;
                MediaElement_Trailer.Play();
            }
            catch (Exception)
            {
            }

        }

        private void CleanPelicula()
        {
            pelicula = new Pelicula();
            GridFilms.DataContext = pelicula;
        }
        private void CleanProveedor()
        {
            proveedor = new Proveedor();
            GridProvider.DataContext = proveedor;
        }

        //Cleans all the texboxes from Peliculas
        private void CleanTextboxPelicula()
        {
            textbox_NameFilm.Text = "";
            textbox_PosterFilm.Text = "";
            textbox_TypeFilm.Text = "";
            textbox_TrailerFilm.Text = "";
            textbox_YearFilm.Text = "";
            textbox_PriceFilm.Text = "0";
            Image_VideoFake.Visibility = Visibility.Visible;
            MediaElement_Trailer.Stop();
            MediaElement_Trailer.Visibility = Visibility.Hidden;
        }

        //Cleans all the textboxes from Proveedores
        private void CleanTextboxProveedor()
        {
            textbox_NameProvider.Text = "";
            textbox_AddressProvider.Text = "";
            textbox_CountryProvider.Text = "";
            textbox_PhoneProvider.Text = "";
            textbox_EmailProvider.Text = "";
        }

        //Loads the IdProvider Combobox
        private void LoadComboboxProviderId()
        {
            textbox_ProducerFilm.Items.Clear();
            List<Proveedor> listaProveedores = uow.RepositorioProveedor.ObtenerTodo().ToList();
            foreach (var item in listaProveedores)
            {
                textbox_ProducerFilm.Items.Add(item.ProveedorId);
            }
        }

        //Hides all the Tabs
        private void UnloadAll()
        {
            TabCartelera.Visibility = Visibility.Hidden;
            TabAlquileres.Visibility = Visibility.Hidden;
            TabTuCartelera.Visibility = Visibility.Hidden;
            TabTuAlquiler.Visibility = Visibility.Hidden;
            TabPeliculas.Visibility = Visibility.Hidden;
            TabProveedores.Visibility = Visibility.Hidden;
            TabSuperAdmin.Visibility = Visibility.Hidden;
        }

        //Makes the normal user tabs visibles
        private void LoadNormal()
        {
            TabCartelera.Visibility = Visibility.Visible;
            TabAlquileres.Visibility = Visibility.Visible;
            TabTuCartelera.Visibility = Visibility.Visible;
            TabTuAlquiler.Visibility = Visibility.Visible;
        }

        //Makes the admin user tabs visibles
        private void LoadAdmin()
        {
            TabCartelera.Visibility = Visibility.Visible;
            TabAlquileres.Visibility = Visibility.Visible;
            TabTuCartelera.Visibility = Visibility.Visible;
            TabTuAlquiler.Visibility = Visibility.Visible;
            TabPeliculas.Visibility = Visibility.Visible;
            TabProveedores.Visibility = Visibility.Visible;
        }

        //Makes the SuperAdmin user tabs visibles
        private void LoadSuperadmin()
        {
            TabCartelera.Visibility = Visibility.Visible;
            TabAlquileres.Visibility = Visibility.Visible;
            TabTuCartelera.Visibility = Visibility.Visible;
            TabTuAlquiler.Visibility = Visibility.Visible;
            TabPeliculas.Visibility = Visibility.Visible;
            TabProveedores.Visibility = Visibility.Visible;
            TabSuperAdmin.Visibility = Visibility.Visible;
        }

        //This checks the Type of User
        private void CheckUser()
        {
            if (rol.Equals("SuperAdmin"))
            {
                LoadSuperadmin();
            }
            else if (rol.Equals("Admin"))
            {
                LoadAdmin();
            }
            else { LoadNormal(); }
        }


        #endregion Methods


    }
}
