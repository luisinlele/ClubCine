﻿using ProyectoFinal.BD.DAL;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace ProyectoFinal.Salas
{
    /// <summary>
    /// Lógica de interacción para Auditorium1Window.xaml
    /// </summary>
    public partial class Auditorium1Window : Window
    {
        public static Uri uriGreen = new Uri("Images/Icons/seatG.png", UriKind.Relative);
        public static Uri uriYellow = new Uri("Images/Icons/seatY.png", UriKind.Relative);
        public static Uri uriRed = new Uri("Images/Icons/seatR.png", UriKind.Relative);

        public static StreamResourceInfo streamInfoGreen = Application.GetResourceStream(uriGreen);
        public static StreamResourceInfo streamInfoYellow = Application.GetResourceStream(uriYellow);
        public static StreamResourceInfo streamInfoRed = Application.GetResourceStream(uriRed);

        BitmapFrame green = BitmapFrame.Create(streamInfoGreen.Stream);
        BitmapFrame yellow = BitmapFrame.Create(streamInfoYellow.Stream);
        BitmapFrame red = BitmapFrame.Create(streamInfoRed.Stream);

        UnitOfWork uow = new UnitOfWork();

        List<Asiento> asientos;

        Usuario usuario;

        MainWindow mainWindow;

        Reserva reserva = new Reserva();
        int contadorAsientos = 0;

        public Auditorium1Window(Usuario usuario, MainWindow main)
        {
            InitializeComponent();
            CheckChairs();
            this.usuario = usuario;
            //Create();
            mainWindow = main;
        }

        private void MarkChair(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var brush = new ImageBrush();
            
           
            if(button.Tag == null)
            {
                button.Tag = "marcado";
                brush.ImageSource = yellow;
            }
            else
            {
                button.Tag = null;
                brush.ImageSource = green;
            }
            
            button.Background = brush;
            
            Console.WriteLine(button.Name); //esto nos saca el nombre del boton!!!
        }

        public void CheckChairs()
        {
            foreach(Asiento asiento in uow.RepositorioAsiento.ObtenerVarios(x => x.SalaIdAsiento == 1))
            {
                if (asiento.OcupadoAsiento)
                {
                    foreach(UIElement element in Auditorium1Grid.Children)
                    {
                        if (element.GetType().ToString().Equals("System.Windows.Controls.Button"))
                        {
                            Button button = element as Button;
                            if (button.Name.Equals(asiento.NumeroAsiento))
                            {
                                button.IsEnabled = false;
                                var brush = new ImageBrush();
                                brush.ImageSource = red;
                                button.Background = brush;
                            }
                        }
                    }
                }
            }
        }
        
        private void Create()
        {
            foreach(UIElement element in Auditorium1Grid.Children)
            {
                if (element.GetType().ToString().Equals("System.Windows.Controls.Button"))
                {
                    Button button = element as Button;
                    Asiento asiento = new Asiento();

                    asiento.NumeroAsiento = button.Name;
                    asiento.SalaIdAsiento = 1;
                    uow.RepositorioAsiento.Crear(asiento);
                    
                }
            }
        }

        private void button_ReservationAuditorium1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres hacer esta reserva?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    foreach (UIElement element in Auditorium1Grid.Children)
                    {
                        if (element.GetType().ToString().Equals("System.Windows.Controls.Button"))
                        {
                            Button button = element as Button;
                            if (button.Tag != null)
                            {
                                contadorAsientos++;
                                Asiento asiento = uow.RepositorioAsiento.ObtenerUno(c => c.NumeroAsiento.Equals(button.Name) && c.SalaIdAsiento == 1);
                                asiento.OcupadoAsiento = true;
                                uow.RepositorioAsiento.Actualizar(asiento);
                            }
                        }
                    }
                    if (contadorAsientos != 0)
                    {
                        Pelicula pelicula = uow.RepositorioPelicula.ObtenerUno(c => c.NombrePelicula.Equals("Big Fish"));

                        reserva.UsuarioIdReserva = Convert.ToInt32(usuario.UsuarioId);
                        reserva.PeliculaPrecioReserva = Convert.ToInt32(pelicula.PrecioPelicula) * contadorAsientos;
                        DateTime data = new DateTime();
                        data = DateTime.Today;
                        reserva.FechaReserva = data.ToString("dd/MM/yyyy");
                        reserva.HoraReserva = "22:00";
                        reserva.SalaIdReserva = 1;
                        reserva.HabilitadoReserva = true;
                        uow.RepositorioReserva.Crear(reserva);
                        MessageBoxResult confirmation = MessageBox.Show("Reserva hecha Correctamente. Debes " + 10 * contadorAsientos, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxResult confirmation = MessageBox.Show("No has seleccionado ningún asiento.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    mainWindow.CargarReservasPorUser();
                    this.Close();
                    break;
            }
        }
    }
}
