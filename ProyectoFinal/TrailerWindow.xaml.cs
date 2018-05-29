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
    /// Lógica de interacción para TrailerWindow.xaml
    /// </summary>
    public partial class TrailerWindow : Window
    {
        
        
        public TrailerWindow()
        {
            InitializeComponent();
            MediaElement_Trailer.Stop();
        }

        private void button_PlayTrailer_Click(object sender, RoutedEventArgs e)
        {

            MediaElement_Trailer.Play();
            
        }

        private void button_PauseTrailer_Click(object sender, RoutedEventArgs e)
        {
            MediaElement_Trailer.Pause();
        }



        private void ShowTrailer(string ruta)
        {
            try
            {
                Uri pathUri = new Uri(ruta);
                MediaElement_Trailer.Source = pathUri;
                MediaElement_Trailer.Width = 368.8;
                MediaElement_Trailer.Height = 288;
            }
            catch (Exception)
            {
            }

        }

    }
}
