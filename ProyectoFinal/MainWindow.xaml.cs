using System;
using System.Collections.Generic;
using System.IO;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
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

        //RentData Window
        RentdataWindow rentWindow;

        //UpdateUser Window
        UserUpdateWindow updateUserWindow;

        //Explorer
        OpenFileDialog explorador = new OpenFileDialog();

        //The Unit Of Work
        UnitOfWork uow = new UnitOfWork();

        //Pelicula
        Pelicula pelicula = new Pelicula();

        //Proveedor
        Proveedor proveedor = new Proveedor();

        //User that signs in
        Usuario usuario;

        //Usuarios
        Usuario user = new Usuario();

        //User that shows on "SuperAdmin"
        Usuario userActualizar = new Usuario();

        //The rents of the User
        Alquiler alquiler = new Alquiler();

        //The rol of the user
        String rol;

        //This represents the trailer that TrailerWindow has to load
        String trailerForWindow;

        //This boolean tells us if the Preview of the trailer is loaded on the Film Window
        bool TrailerPreviewLoaded = false;

        //This boolean tells us if the Preview of the Trailer is Playing on the film window
        bool TrailerPlaying = true;

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            //hide tabs
            UnloadAll();

            GridFilms.DataContext = pelicula;
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerVarios(c => c.HabilitadoPelicula == true);

            //loads all the providersIds in the combobox of films
            LoadComboboxProviderId();

            //makes all the textboxes on films empty
            CleanTextboxPelicula();

            GridProvider.DataContext = proveedor;
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerVarios(c => c.HabilitadoProveedor == true);

            //makes all the textboxes on providers empty
            CleanTextboxProveedor();

            this.usuario = usuario;

            //checks the email and the profile picture to load them in the Window
            CheckUser();

            rol = usuario.RolUsuario;

            //checks the role of the user that joined to see witch tabs to make visible
            CheckUserType();

            //loads the datagrid of reservations for the user that singed in
            CargarReservasPorUser();

            //loads the datagrid of rents for the user that signed in
            CargarAlquileresPorUser();

            GridUsuario.DataContext = user;
            datagrid_Users.ItemsSource = uow.RepositorioUsuario.ObtenerTodo().ToList();

            //generates buttons in the rents tab for each film in the database
            GenerarBotonesPeliculas();
        }


        #region Cartelera

        //Methods that open "AuditoriumWindow" depending on the Room
        #region LoadAuditoriums
        
        private void button_Loadsits1Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium1 = new Auditorium1Window(usuario, this);
            auditorium1.Show();
        }
        private void button_Loadsits2Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium2 = new Auditorium2Window(usuario, this);
            auditorium2.Show();
        }

        private void button_Loadsits3artelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium3 = new Auditorium3Window(usuario, this);
            auditorium3.Show();
        }

        private void button_Loadsits4Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium4 = new Auditorium4Window(usuario, this);
            auditorium4.Show();
        }

        private void button_Loadsits5Cartelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium5 = new Auditorium5Window(usuario, this);
            auditorium5.Show();
        }

        private void button_Loadsits6artelera_Click(object sender, RoutedEventArgs e)
        {
            auditorium6 = new Auditorium6Window(usuario, this);
            auditorium6.Show();
        }
        #endregion LoadAuditoriums

        //Methods that open "TrailerWindow" depending on the Film
        #region LoadTrailers
        
        private void filmImage1_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala1";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }

        private void filmImage2_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala2";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }

        private void filmImage3_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala3";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }

        private void filmImage4_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala4";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }

        private void filmImage5_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala5";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }

        private void filmImage6_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            trailerForWindow = "Sala6";
            trailerWindow = new TrailerWindow(trailerForWindow);
            trailerWindow.ShowDialog();
        }
        #endregion LoadTrailers

        //Methods that change the color when you put the mouse on the button of the time
        #region controlbotonesHora
        private void button_Loadsits1Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits1Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits1Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits1Cartelera.Foreground = Brushes.GreenYellow;
        }

        private void button_Loadsits2Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits2Cartelera.Foreground = Brushes.GreenYellow;
        }

        private void button_Loadsits2Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits2Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits3Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits3Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits3Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits3Cartelera.Foreground = Brushes.GreenYellow;
        }

        private void button_Loadsits4Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits4Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits4Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits4Cartelera.Foreground = Brushes.GreenYellow;
        }

        private void button_Loadsits5Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits5Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits5Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits5Cartelera.Foreground = Brushes.GreenYellow;
        }

        private void button_Loadsits6Cartelera_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Loadsits6Cartelera.Foreground = Brushes.Red;
        }

        private void button_Loadsits6Cartelera_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Loadsits6Cartelera.Foreground = Brushes.GreenYellow;
        }
        #endregion controlbotonesHora

        #endregion Cartelera


        #region Peliculas

        //Save Button of Pelicula
        private void button_SaveFilm_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_NameFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_PosterFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Cartel", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_TypeFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Género", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_TrailerFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Trailer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_YearFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Año", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_ProducerFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Id de la Productora", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textbox_PriceFilm.Text == "")
            {
                MessageBoxResult error = MessageBox.Show("No has introducido el Precio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                pelicula.CartelPelicula = textbox_PosterFilm.Text;
                pelicula.TrailerPelicula = textbox_TrailerFilm.Text;
                pelicula.HabilitadoPelicula = true;
                uow.RepositorioPelicula.Crear(pelicula);
                MessageBoxResult confirmation = MessageBox.Show("Película añadida Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerVarios(c => c.HabilitadoPelicula == true);
                CleanTextboxPelicula();
                TrailerPreviewLoaded = false;
                GenerarBotonesPeliculas();
            }
        }

        //Modify Button of Pelicula
        private void button_ModifyFilm_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioPelicula.Actualizar(pelicula);
            datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerVarios(c => c.HabilitadoPelicula == true);
            CleanTextboxPelicula();
            TrailerPreviewLoaded = false;
            MessageBoxResult confirmation = MessageBox.Show("Película modificada Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            GenerarBotonesPeliculas();
        }

        //Delete Button of Pelicula
        private void button_DeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("Vas a eliminar una película, ¿estás seguro?", "ALERTA", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    pelicula.HabilitadoPelicula = false;
                    uow.RepositorioPelicula.Actualizar(pelicula);
                    datagrid_Film.ItemsSource = uow.RepositorioPelicula.ObtenerVarios(c => c.HabilitadoPelicula == true);
                    CleanPelicula();
                    TrailerPreviewLoaded = false;
                    GenerarBotonesPeliculas();
                    break;
            }
                    
        }

        //When you click on data of the Pelicula DataGrid
        private void datagrid_Film_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                pelicula = (Pelicula)datagrid_Film.SelectedItem;
                GridFilms.DataContext = pelicula;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
        //When you click on the button to select a Poster
        private void button_PosterSelect_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_PosterFilm.Text = explorador.FileName.ToString();
            ShowPreviewPoster(textbox_PosterFilm.Text);
        }

        //When you click on the button to select a Trailer
        private void button_TrailerSelect_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "All Video Files(*.MP4;*.3GP;*.WEBM;*.WMV)|*.MP4;*.3GP;*.WEBM;*.WMV|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_TrailerFilm.Text = explorador.FileName.ToString();
            if (textbox_TrailerFilm.Text != "")
            {
                Image_VideoFake.Visibility = Visibility.Hidden;
                MediaElement_Trailer.Visibility = Visibility.Visible;
                ShowPreviewTrailer(textbox_TrailerFilm.Text);
                TrailerPreviewLoaded = true;
            }
        }

        //Button Play below the preview of the Trailer
        private void button_PlayTrailer_Click(object sender, RoutedEventArgs e)
        {
            if (TrailerPreviewLoaded && !TrailerPlaying)
            {
                MediaElement_Trailer.Play();
                TrailerPlaying = true;
            }
        }

        //Button Pause below the preview of the Trailer
        private void button_PauseTrailer_Click(object sender, RoutedEventArgs e)
        {
            if (TrailerPreviewLoaded && TrailerPlaying)
            {
                MediaElement_Trailer.Pause();
                TrailerPlaying = false;
            }
        }

        #endregion Peliculas


        #region Alquileres

        //This generates the buttons depending on the Pelicula data on the database
        public void GenerarBotonesPeliculas()
        {
            List<Pelicula> listapelis = new List<Pelicula>();
            this.AlquileresPeliculas.Children.Clear();
            listapelis = uow.RepositorioPelicula.ObtenerTodo().ToList();
            for (int i = 0; i < listapelis.Count; i++)
            {
                if (listapelis[i].HabilitadoPelicula == true)
                {
                    WrapPanel stack = new WrapPanel();
                    stack.Orientation = Orientation.Horizontal;
                    stack.Width = 255;
                    stack.Height = 396;
                    stack.Margin = new Thickness(30);

                    TextBlock label = new TextBlock();
                    label.Text = listapelis[i].NombrePelicula + " - " + listapelis[i].AñoPelicula;
                    label.TextAlignment = TextAlignment.Center;
                    label.Width = 255;
                    label.Height = 26;
                    label.FontSize = 20;
                    
                    TextBlock label2 = new TextBlock();
                    label2.Text = listapelis[i].PrecioPelicula + " €";
                    label2.TextAlignment = TextAlignment.Center;
                    label2.Width = 255;
                    label2.Height = 27;
                    label2.FontSize = 20;
                    label2.Foreground = Brushes.Lime;

                    Button boton = new Button();
                    boton.Width = 248;
                    boton.Height = 348;
                    //boton.Margin = new Thickness(1);
                    boton.Content = EnseñarCartel(listapelis[i].CartelPelicula);
                    boton.Name = "buttonTPV_" + listapelis[i].PeliculaId;
                    boton.Click += peli_click;

                    stack.Children.Add(boton);
                    stack.Children.Add(label);
                    stack.Children.Add(label2);
                    AlquileresPeliculas.Children.Add(stack);
                }
            }
        }

        //Method that opens an image
        private System.Windows.Controls.Image EnseñarCartel(string ruta)
        {
            try
            {
                System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
                BitmapImage bit3 = new BitmapImage();
                bit3.BeginInit();
                bit3.UriSource = new Uri(ruta);
                bit3.EndInit();
                imagen.Source = bit3;
                imagen.Width = 248;
                imagen.Height = 348;
                return imagen;
            }
            catch (Exception)
            {
                return null;
            }

        }

        //For when you click a generated button of "GenerarBotonesPeliculas()"
        public void peli_click(object sender, RoutedEventArgs e)
        {
            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                Button boton = (Button)aux;
                String[] btname = boton.Name.Split('_');
                int idPeli = Convert.ToInt32(btname[1].Trim());

                Pelicula aReservar = uow.RepositorioPelicula.ObtenerUno(c => c.PeliculaId.Equals(idPeli));

                MessageBoxResult confirmation = MessageBox.Show("¿Quieres alquilar esta película? ", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (confirmation)
                {
                    case MessageBoxResult.Yes:
                        rentWindow = new RentdataWindow(usuario, aReservar, this);
                        rentWindow.ShowDialog();
                        break;
                }
             }
        }

        //Method that Loads the Rents on the datagrid for the user that is Signed In
        public void CargarAlquileresPorUser()
        {
            List<Alquiler> listaAlquileres = new List<Alquiler>();
            listaAlquileres = uow.RepositorioAlquiler.ObtenerVarios(c => c.UsuarioIdReserva == usuario.UsuarioId && c.HabilitadoAlquiler == true);
            datagrid_Alquileres.ItemsSource = uow.RepositorioAlquiler.ObtenerVarios(c => c.UsuarioIdReserva == usuario.UsuarioId && c.HabilitadoAlquiler == true);
            bool isEmpty = !listaAlquileres.Any();

            if (isEmpty)
            {
                label_AlquileresError.Visibility = Visibility.Visible;
                datagrid_Alquileres.Visibility = Visibility.Hidden;
                button_ReturnRent.Visibility = Visibility.Hidden;
                label_Alquilereslbl1.Visibility = Visibility.Hidden;
                label_Alquilereslbl2.Visibility = Visibility.Hidden;
            }
            else
            {
                label_AlquileresError.Visibility = Visibility.Hidden;
                datagrid_Alquileres.Visibility = Visibility.Visible;
                button_ReturnRent.Visibility = Visibility.Visible;
                label_Alquilereslbl1.Visibility = Visibility.Visible;
                label_Alquilereslbl2.Visibility = Visibility.Visible;
            }
        }

        private void button_ReturnRent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("¿Quieres devolver este alquiler? ", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    DateTime data = new DateTime();
                    data = DateTime.Now;
                    alquiler.FechaDevolucionAlquiler = data.ToShortDateString();
                    CargarAlquileresPorUser();
                    uow.RepositorioAlquiler.Actualizar(alquiler);
                    break;
            }            
        }

        private void datagrid_Alquileres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                alquiler = (Alquiler)datagrid_Alquileres.SelectedItem;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }


        #endregion Alquileres


        #region Reservas

        //Method that Loads the reservations on the datagrid for the user that is Signed In
        public void CargarReservasPorUser()
        {
            List<Reserva> listaReservas = new List<Reserva>();
            listaReservas = uow.RepositorioReserva.ObtenerVarios(c => c.UsuarioIdReserva == usuario.UsuarioId && c.HabilitadoReserva == true);
            datagrid_Book.ItemsSource = uow.RepositorioReserva.ObtenerVarios(c => c.UsuarioIdReserva == usuario.UsuarioId && c.HabilitadoReserva == true);
            bool isEmpty = !listaReservas.Any();

            if (isEmpty)
            {
                label_ReservasError.Visibility = Visibility.Visible;
                datagrid_Book.Visibility = Visibility.Hidden;
                label_Reservaslbl1.Visibility = Visibility.Hidden;
                label_Reservaslbl2.Visibility = Visibility.Hidden;
            }
            else
            {
                label_ReservasError.Visibility = Visibility.Hidden;
                datagrid_Book.Visibility = Visibility.Visible;
                label_Reservaslbl1.Visibility = Visibility.Visible;
                label_Reservaslbl2.Visibility = Visibility.Visible;
            }
        }

        #endregion Reservas


        #region Proveedores

        //Save Button of Proveedor
        private void button_SaveProvider_Click(object sender, RoutedEventArgs e)
        {
            proveedor.HabilitadoProveedor = true;
            uow.RepositorioProveedor.Crear(proveedor);
            MessageBoxResult confirmation = MessageBox.Show("Proveedor añadido Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerVarios(c => c.HabilitadoProveedor == true);
            LoadComboboxProviderId();
            CleanTextboxProveedor();
        }

        //Modify Button of Proveedor
        private void button_ModifyProvider_Click(object sender, RoutedEventArgs e)
        {
            uow.RepositorioProveedor.Actualizar(proveedor);
            datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerVarios(c => c.HabilitadoProveedor == true);
            MessageBoxResult confirmation = MessageBox.Show("Proveedor modificado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            CleanTextboxProveedor();
        }

        //Delete Button of Proveedor
        private void button_DeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("Vas a eliminar un proveedor, ¿estás seguro?", "ALERTA", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    proveedor.HabilitadoProveedor = false;
                    uow.RepositorioProveedor.Actualizar(proveedor);
                    datagrid_Provider.ItemsSource = uow.RepositorioProveedor.ObtenerVarios(c => c.HabilitadoProveedor == true);
                    CleanProveedor();
                    break;
            }
                    
        }

        //When you click on data of the Proveedor DataGrid
        private void datagrid_Provider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                proveedor = (Proveedor)datagrid_Provider.SelectedItem;
                GridProvider.DataContext = proveedor;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        #endregion Proveedores


        #region General

        //Method that opens the LoginWindow when this MainWindow gets closed
        private void WindowClosed(object sender, EventArgs e)
        {
            loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        //When you click on the button that opens the Window to Update Users
        private void button_UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            updateUserWindow = new UserUpdateWindow(usuario);
            updateUserWindow.ShowDialog();
        }

        //When you Click to close the window
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("¿Estás seguro de que quieres cerrar sesión?", "CONFIRMACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (confirmation)
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
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
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

        }


        //Shows a preview of the Film's Trailer
        private void ShowPreviewTrailer(string ruta)
        {
            try
            {
                Uri pathUri = new Uri(ruta);
                MediaElement_Trailer.Source = pathUri;
                MediaElement_Trailer.Width = 157;
                MediaElement_Trailer.Height = 95;
                MediaElement_Trailer.Play();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

        }

        //Method that cleans "GridFilms"
        private void CleanPelicula()
        {
            pelicula = new Pelicula();
            GridFilms.DataContext = pelicula;
        }

        //Method that cleans "GridProvider"
        private void CleanProveedor()
        {
            proveedor = new Proveedor();
            GridProvider.DataContext = proveedor;
        }

        //Cleans all the texboxes from Peliculas
        private void CleanTextboxPelicula()
        {
            textbox_PosterFilm.Text = "";
            textbox_NameFilm.Text = "";
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
            List<Proveedor> listaProveedores = uow.RepositorioProveedor.ObtenerVarios(c => c.HabilitadoProveedor == true).ToList();
            foreach (var item in listaProveedores)
            {
                textbox_ProducerFilm.Items.Add(item.ProveedorId);
            }
        }

        #region ControlPestañas
        
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

        #endregion ControlPestañas

        //Method that changes the email and the profile picture of the window to the ones of the User that singed in
        private void CheckUser()
        {
            try
            {
                label_UserEmail.Text = "Usuario: " + usuario.EmailUsuario;
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(usuario.PerfilUsuario);
                bit.EndInit();
                image_UserProfile.Source = bit;
                image_UserProfile.Width = 115;
                image_UserProfile.Height = 110;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error!: " +error.ToString());
            }
        }


        //Method that checks the Type of User
        private void CheckUserType()
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


        #region SuperUser

        //Upgrades an user to the next role from "Normal" to "Admin" to "SuperAdmin"
        private void button_UpgradeUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("¿Seguro que quieres subir de Rango a " +userActualizar.NombreUsuario+ "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    if (userActualizar.RolUsuario == "Admin")
                    {
                        userActualizar.RolUsuario = "SuperAdmin";
                    }
                    else userActualizar.RolUsuario = "Admin";

                    uow.RepositorioUsuario.Actualizar(userActualizar);
                    datagrid_Users.ItemsSource = uow.RepositorioUsuario.ObtenerTodo().ToList();
                    break;
            }
        }

        //when you select an item in "datagrid_Users"
        private void datagrid_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                userActualizar = (Usuario)datagrid_Users.SelectedItem;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        //Deletes or 'bans' an user
        private void button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("Vas a deshabilitar a un usuario, ¿estás seguro?", "ALERTA", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    usuario.HabilitadoUsuario = false;
                    uow.RepositorioUsuario.Crear(usuario);
                    datagrid_Users.ItemsSource = uow.RepositorioUsuario.ObtenerTodo().ToList();
                    break;
            }
                    
        }

        //Checks if there is some user in the database whose name is the same as in "textbox_SearchUser.Text"
        private void button_SearchUser_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = uow.RepositorioUsuario.ObtenerUno(c => c.NombreUsuario.Equals(textbox_SearchUser.Text));
            if (user != null)
            {
                datagrid_Users.ItemsSource = uow.RepositorioUsuario.ObtenerVarios(c => c.NombreUsuario.Equals(textbox_SearchUser.Text));
            }
            else
            {
                MessageBoxResult error = MessageBox.Show("No existe ningún usuario con ese nombre.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        //Loads all the users in the dataGrid
        private void button_LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Users.ItemsSource = uow.RepositorioUsuario.ObtenerTodo().ToList();
        }

        #endregion SuperUser


        //nada
        #region testss 



        #endregion testss


    }
}
