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
    /// Lógica de interacción para crearClase.xaml
    /// </summary>
    public partial class crearClase : Window
    {
        IFirebaseConfig config = new FirebaseConfig

        {
            AuthSecret = "b4EKkNKwSvFpmAdcRdMRWPv90myyYgIirOv6QULs",
            BasePath = "https://horsebooking-54bbe-default-rtdb.europe-west1.firebasedatabase.app"
        };
        IFirebaseClient client;

        public crearClase()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error en la conexión a Firebase");
            }
        }
        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            // Código para mostrar la página de inicio
        }

        private void Novedades_Click(object sender, RoutedEventArgs e)
        {
            var novedades = new Novedades();
            novedades.Show();
            this.Close();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            var chat = new Chat();
            chat.Show();
            this.Close();
        }

        private void PerfilUsuario_Click(object sender, RoutedEventArgs e)
        {
            var perfil = new PerfilUsuario();
            perfil.Show();
            this.Close();
        }

        private async void crearClase_Click(object sender, RoutedEventArgs e)
        {
            string tituloClase = textBoxTituloClase.Text.ToString();
            string descripcion = textBoxDescripcionClase.Text.ToString();
            string fecha_fin = fechaFinDatePicker.ToString();
            string fecha_inicio = fechaInicioDatePicker.ToString();
            int hora = 0, minuto = 0;
            bool horaValida = int.TryParse(comboBoxHora.SelectedItem?.ToString() ?? "", out hora);
            bool minutoValido = int.TryParse(comboBoxMinutos.SelectedItem?.ToString() ?? "", out minuto);
            int precio;
            bool precioValido = int.TryParse(precioTextBox.Text, out precio);
            bool resultado = int.TryParse(precioTextBox.Text, out precio);
            string tipo = comboBoxDisciplina.SelectedItem.ToString();
            string profesor = comboBoxProfesor.SelectedItem.ToString();

            if (!horaValida || !minutoValido || !precioValido)
            {
                MessageBox.Show("Por favor, verifica que todos los campos están correctos y completos.");
                return;
            }

            try
            {
                FirebaseResponse response = await client.PushAsync("clases/", new
                {
                    titulo = tituloClase,
                    descripcion = descripcion,
                    fecha_fin = fecha_fin,
                    fecha_inicio = fecha_inicio,
                    precio = precio,
                    tipo = tipo,
                    hora = horaValida,
                    minuto = minutoValido,
                    profesor = profesor
                });
            }
            catch
            {
                MessageBox.Show("Error al subir la clase");
            }
        }
    }
}
