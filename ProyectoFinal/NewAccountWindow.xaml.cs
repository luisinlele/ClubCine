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
    /// Lógica de interacción para NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        //Unit Of Work
        UnitOfWork uow = new UnitOfWork();

        //Usuario
        Usuario usuario = new Usuario();


        OpenFileDialog explorador = new OpenFileDialog();
        
        public NewAccountWindow()
        {
            InitializeComponent();

            GridUser.DataContext = usuario;

            //This sets the password Error Label to hidden everytime the application starts
            label_PasswordError.Visibility = Visibility.Hidden;
        }


        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxOpen();
        }

        private void MessageBoxOpen()
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
            }
        }

        private void button_SelectProfilePic_Click(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            textbox_ProfilePic.Text = explorador.FileName.ToString();
            ShowPreview(textbox_ProfilePic.Text);
        }

        private void ShowPreview(string ruta)
        {
            try
            {
                BitmapImage bit2 = new BitmapImage();
                bit2.BeginInit();
                bit2.UriSource = new Uri(ruta);
                bit2.EndInit();
                image_ProfilePicPreview.Source = bit2;
                image_ProfilePicPreview.Width = 218;
                image_ProfilePicPreview.Height = 256;
            }
            catch (Exception)
            {
            }

        }

        private void button_SaveNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_PasswordNewAccount.Password == textbox_PasswordcheckNewAccount.Password)
            {
                List<Usuario> userCheckEmail = new List<Usuario>();
                List<Usuario> userCheckPhone = new List<Usuario>();
                userCheckEmail = uow.RepositorioUsuario.ObtenerVarios(c => c.EmailUsuario.Equals(textbox_EmailNewAccount.Text));
                userCheckPhone = uow.RepositorioUsuario.ObtenerVarios(c => c.TelefonoUsuario.Equals(textbox_PhoneNewAccount.Text));
                bool isEmpty = !userCheckEmail.Any();
                bool isEmptyToo = !userCheckPhone.Any();
                if (isEmpty)
                {
                    if (isEmptyToo)
                    {
                        label_PasswordError.Visibility = Visibility.Hidden;
                        DateTime data = new DateTime();
                        data = DateTime.Today;
                        usuario.FechaCreacionUsuario = data;
                        usuario.ContraseñaUsuario = textbox_PasswordNewAccount.Password;
                        usuario.RolUsuario = "Normal";
                        usuario.PerfilUsuario = textbox_ProfilePic.Text;
                        usuario.HabilitadoUsuario = true;
                        uow.RepositorioUsuario.Crear(usuario);
                        MessageBoxResult confirmation = MessageBox.Show("Perfil creado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        switch (confirmation)
                        {
                            case MessageBoxResult.OK:
                                this.Close();
                                break;
                        }
                    }
                    else
                    {
                        MessageBoxResult errorPhone = MessageBox.Show("Ya existe un usuario con ese Teléfono", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBoxResult errorEmail = MessageBox.Show("Ya existe un usuario con ese correo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                
            }
            else
            {
                label_PasswordError.Visibility = Visibility.Visible;
            }
        }

    }
}
