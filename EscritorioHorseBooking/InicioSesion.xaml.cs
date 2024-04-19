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

        private IFirebaseConfig config;

        public InicioSesion()
        {
            InitializeComponent();
            this.config = config;
        }

        private void buttonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string email = "almudena@gmail.com";
            string contraseña = "Abcde123";

            // Comprueba si el email y la contraseña no están vacíos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, introduce tu email y contraseña.");
                return;
            }

            // Accede a la base de datos en tiempo real
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            client.GetAsync("trabajadores/" + email.Replace('.', ',')).ContinueWith(async (task) =>
            {
                FirebaseResponse response = task.Result;

                // Comprueba si el usuario existe en la base de datos
                if (response.Body != "null")
                {
                    // El usuario existe, ahora debes comprobar la contraseña
                    var trabajador = response.ResultAs<Dictionary<string, string>>();
                    if (trabajador["Password"] == contraseña)
                    {
                        MessageBox.Show("Inicio de sesión exitoso.");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                        // Aquí puedes abrir la ventana principal de la aplicación o realizar otras acciones
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
            });
        }


    }
}
