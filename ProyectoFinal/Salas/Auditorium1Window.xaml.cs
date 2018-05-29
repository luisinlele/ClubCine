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

        public Auditorium1Window(Usuario usuario)
        {
            InitializeComponent();
            CheckChairs();

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

        //var brush = new ImageBrush();
        //brush.ImageSource = temp;
        //    silla2.Background = brush;


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
            var brushTest = new ImageBrush();
            brushTest.ImageSource = yellow;

            foreach (UIElement element in Auditorium1Grid.Children)
            {
                if (element.GetType().ToString().Equals("System.Windows.Controls.Button"))
                {
                    Button button = element as Button;
                    if (button.Tag != null)
                    {
                        
                        Asiento asiento = uow.RepositorioAsiento.ObtenerUno(c => c.NumeroAsiento.Equals(button.Name) && c.SalaIdAsiento == 1);
                        asiento.OcupadoAsiento = true;
                        uow.RepositorioAsiento.Actualizar(asiento);
                    }
                }
            }


        }
    }
}
