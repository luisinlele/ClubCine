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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //The Unit Of Work
        UnitOfWork uow = new UnitOfWork();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void button_NewAccountMain_Click(object sender, RoutedEventArgs e)
        {
            NewAccountWindow newAccountWindow = new NewAccountWindow();
            newAccountWindow.ShowDialog();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = uow.RepositorioUsuario.ObtenerUno(c => c.EmailUsuario.Equals(textbox_EmailLogin.Text));
            if (user != null)
            {

                if (user.ContraseñaUsuario.Equals(textbox_PassLogin.Password))
                {
                    System.Windows.MessageBox.Show("Bienvenido :)", "Saludos", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow main = new MainWindow(user);
                    main.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Error! Contraseña Incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Error! Email o Contraseña Incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

            textbox_EmailLogin.Text = "";
            textbox_PassLogin.Password = "";
        }
        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
