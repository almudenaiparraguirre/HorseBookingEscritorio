using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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

namespace EscritorioHorseBooking
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {

        public IFirebaseConfig config = new FirebaseConfig

        {
            AuthSecret = "b4EKkNKwSvFpmAdcRdMRWPv90myyYgIirOv6QULs",
            BasePath = "https://horsebooking-54bbe-default-rtdb.europe-west1.firebasedatabase.app"
        };
        public IFirebaseClient client;

        public InicioSesion()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error en la conexión a Firebase");
            }
        }

        private async void buttonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string emailText = email.Text.ToString();
            string contraseñaText = contrasena.Password;

            if (string.IsNullOrEmpty(emailText) || string.IsNullOrEmpty(contraseñaText))
            {
                MessageBox.Show("Por favor, introduce tu email y contraseña.");
                return;
            }

            // Accede a la base de datos en tiempo real
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            try
            {
                FirebaseResponse response = await client.GetAsync("trabajadores/" + emailText.Replace('.', ','));

                // Comprueba si el usuario existe en la base de datos
                if (response.Body != "null")
                {
                    var trabajador = response.ResultAs<Dictionary<string, string>>();
                    if (trabajador["Password"] == contraseñaText)
                    {
                        // Dispatch UI updates back to the UI thread
                        Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show("Inicio de sesión exitoso.");
                            PantallaInicial pantallaInicial = new PantallaInicial();
                            pantallaInicial.Show();
                            this.Close();
                        });
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta.");
                    }
                }
                else
                {
                    MessageBox.Show("El usuario no existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to log in: " + ex.Message);
            }
        }

        private void goToRegistro_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }

}