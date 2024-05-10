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
    /// Lógica de interacción para Novedades.xaml
    /// </summary>
    public partial class Novedades : Window
    {
        IFirebaseConfig config = new FirebaseConfig

        {
            AuthSecret = "b4EKkNKwSvFpmAdcRdMRWPv90myyYgIirOv6QULs",
            BasePath = "https://horsebooking-54bbe-default-rtdb.europe-west1.firebasedatabase.app"
        };
        IFirebaseClient client;

        public Novedades()
        {
            InitializeComponent();

            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error en la conexión a Firebase");
            }
        }

        private async void crearNovedad_Click(object sender, RoutedEventArgs e)
        {
            string tituloNovedad = textBoxtituloNoticia.Text.ToString();
            string descripcion = textBoxdescNoticia.Text.ToString();

            FirebaseResponse response = await client.PushAsync("novedades/", new { titulo = tituloNovedad, descripcion = descripcion, fecha = DateTime.Now });
        }

        private async void crearClase_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = "clase creada desde escritorio";
            string fecha_fin = "mañana";
            string fecha_inicio = "hoy";
            int precio = 25;
            string tipo = "Doma";
            string tituloClase = "Clase avanzada de doma";

            FirebaseResponse response = await client.PushAsync("clases/", new { titulo = tituloClase, 
                descripcion = descripcion, fecha_fin = fecha_fin, fecha_inicio = fecha_inicio,
                precio = precio, tipo = tipo});
        }
        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            var pantalla = new PantallaInicial();
            pantalla.Show();
            this.Close();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            var chat = new Chat();
            chat.Show();
            this.Close();
        }

        private void Clases_Click(object sender, RoutedEventArgs e)
        {
            var crearClase = new crearClase();
            crearClase.Show();
            this.Close();
        }

        private void PerfilUsuario_Click(object sender, RoutedEventArgs e)
        {
            var perfil = new PerfilUsuario();
            perfil.Show();
            this.Close();
        }
    }
}
