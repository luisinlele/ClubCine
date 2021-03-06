﻿using Microsoft.Win32;
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
    /// Lógica de interacción para UserUpdateWindow.xaml
    /// </summary>
    public partial class UserUpdateWindow : Window
    {
        Usuario user = new Usuario();

        UnitOfWork uow = new UnitOfWork();

        //Explorer
        OpenFileDialog explorador = new OpenFileDialog();

        public UserUpdateWindow(Usuario usuario)
        {
            user = uow.RepositorioUsuario.ObtenerUno(c => c.UsuarioId == usuario.UsuarioId);
            InitializeComponent();
            UserUpdate.DataContext = user;
            textbox_PasswordUpdate.Password = user.ContraseñaUsuario;
            CargarImagenPerfil(user.PerfilUsuario);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_PasswordUpdate.Password.Equals(textbox_PasswordcheckUpdate.Password))
            {
                user.ContraseñaUsuario = textbox_PasswordUpdate.Password;
                label_PasswordError.Visibility = Visibility.Hidden;
                uow.RepositorioUsuario.Actualizar(user);
                MessageBoxResult confirmation = MessageBox.Show("Su perfil se ha modificado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                label_PasswordError.Visibility = Visibility.Visible;
            }

            
        }

        private void CargarImagenPerfil(string path)
        {
            try
            {
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(path);
                bit.EndInit();
                image_ProfilePicPreview.Source = bit;
                image_ProfilePicPreview.Width = 153;
                image_ProfilePicPreview.Height = 181;
            }
            catch (Exception)
            {
            }
        }

        private void button_SelectProfilePic_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_ProfilePic.Text = explorador.FileName.ToString();
            CargarImagenPerfil(textbox_ProfilePic.Text);
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
            }
        }
    }
}
