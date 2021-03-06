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

namespace ProyectoFinal
{
    /// <summary>
    /// Lógica de interacción para TrailerWindow.xaml
    /// </summary>
    public partial class TrailerWindow : Window
    {
        String trailer, path;
        bool isDark = false;

        public TrailerWindow(String trailerForWindow)
        {
            InitializeComponent();
            //MediaElement_Trailer.Stop();
            this.trailer = trailerForWindow;
            CheckFilm(trailer);
            ShowTrailer(path);
            MediaElement_Trailer.Play();
            rectangle.Opacity = 0;
        }

        private void button_PlayTrailer_Click(object sender, RoutedEventArgs e)
        {

            MediaElement_Trailer.Play();
            
        }

        private void button_PauseTrailer_Click(object sender, RoutedEventArgs e)
        {
            MediaElement_Trailer.Pause();
        }

        private void CheckFilm(String trailer)
        {
            switch (trailer)
            {
                case "Sala1":
                    path = "Videos/BigFish.mp4";
                    break;

                case "Sala2":
                    path = "Videos/12AngryMen.mp4";
                    break;

                case "Sala3":
                    path = "Videos/Hannah.mp4";
                    break;

                case "Sala4":
                    path = "Videos/Jaws.mp4";
                    break;

                case "Sala5":
                    path = "Videos/GoodFellas.mp4";
                    break;

                case "Sala6":
                    path = "Videos/Memento.mp4";
                    break;
            }
        }

        private void button_LightsOut_Click(object sender, RoutedEventArgs e)
        {
            if (!isDark)
            {
                rectangle.Opacity = 0.8;
                isDark = true;
                button_LightsOut.Content = "Encender Luces";
            }
            else
            {
                rectangle.Opacity = 0;
                isDark = false;
                button_LightsOut.Content = "Apagar Luces";
            }
            
            
        }

        private void ShowTrailer(string ruta)
        {
            try
            {
                Uri pathUri = new Uri(ruta, UriKind.Relative);
                MediaElement_Trailer.Source = pathUri;
                MediaElement_Trailer.Width = 623;
                MediaElement_Trailer.Height = 392;
            }
            catch (Exception e)
            {
            }

        }

    }
}
