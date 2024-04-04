using FireSharp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscritorioHorseBooking
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFirebaseConfig config = new FirebaseConfig

        {
            AuthSecret = "b4EKkNKwSvFpmAdcRdMRWPv90myyYgIirOv6QULs",
            BasePath = "https://horsebooking-54bbe-default-rtdb.europe-west1.firebasedatabase.app"
        };
        IFirebaseClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error en la conexión a Firebase");
            }
        }

        private async void registrar_Click(object sender, EventArgs e)
        {
            string email = "almudena@gmail.com";
            string contrasena = "Abcde123";

            // Registrar usuario en Firebase Authentication
            // (Asume que ya tienes configurada la autenticación en Firebase)

            // Añadir usuario a la base de datos en tiempo real
            FirebaseResponse response = await client.SetAsync("usuarios/" + email.Replace('.', ','), new { Email = email, Password = contrasena });

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Usuario registrado correctamente en Firebase");
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario en Firebase");
            }
        }

    }
}
