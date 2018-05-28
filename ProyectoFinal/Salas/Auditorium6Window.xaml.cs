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
    /// Lógica de interacción para Auditorium6Window.xaml
    /// </summary>
    public partial class Auditorium6Window : Window
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

        public Auditorium6Window()
        {
            InitializeComponent();
        }

        private void MarkChair(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var brush = new ImageBrush();

            if (button.Tag == null)
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
    }
}
