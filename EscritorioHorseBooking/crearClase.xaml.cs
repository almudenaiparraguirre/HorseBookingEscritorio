using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Clase> ListaClases { get; set; }
        FirebaseStorage firebaseStorage;

        public crearClase()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            ListaClases = new List<Clase>();
            firebaseStorage = new FirebaseStorage("horsebooking-54bbe.appspot.com", new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("AIzaSyAMBaq52-eEyy_wMCDnTlx3gilW-gXRyfo"),
                ThrowOnCancel = true
            });
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

        public class Clase
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
            public int Hora { get; set; }
            public int Minutos { get; set; }
            public int Precio { get; set; }
            public string Disciplina { get; set; }
            public string Profesor { get; set; }
        }

        private async void VerClases_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("clases/");
                var data = JsonConvert.DeserializeObject<Dictionary<string, Clase>>(response.Body);
                if (data == null)
                {
                    MessageBox.Show("No se encontraron datos.");
                    return;
                }

                ListaClases.Clear();
                foreach (var item in data.Values)
                {
                    ListaClases.Add(item);
                }

                dataGridClases.ItemsSource = ListaClases;
                dataGridClases.Items.Refresh();
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show("Error al parsear los datos de Firebase: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clases desde Firebase: " + ex.Message);
            }
        }

        private async void crearClase_Click(object sender, RoutedEventArgs e)
        {
            string tituloClase = textBoxTituloClase.Text;
            string descripcion = textBoxDescripcionClase.Text;
            string fecha_fin = fechaFinDatePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "";
            string fecha_inicio = fechaInicioDatePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

            if (!(comboBoxHora.SelectedItem is ComboBoxItem horaItem) ||
                !int.TryParse(horaItem.Content.ToString(), out int hora))
            {
                MessageBox.Show("Selecciona una hora válida.");
                return;
            }

            if (!(comboBoxMinutos.SelectedItem is ComboBoxItem minutoItem) ||
                !int.TryParse(minutoItem.Content.ToString(), out int minuto))
            {
                MessageBox.Show("Selecciona un minuto válido.");
                return;
            }

            if (!int.TryParse(precioTextBox.Text, out int precio))
            {
                MessageBox.Show("Ingresa un precio válido.");
                return;
            }

            string tipo = (comboBoxDisciplina.SelectedItem as ComboBoxItem)?.Content.ToString();
            string profesor = (comboBoxProfesor.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(profesor))
            {
                MessageBox.Show("Selecciona un tipo de disciplina y un profesor.");
                return;
            }

            try
            {
                var clase = new
                {
                    titulo = tituloClase,
                    descripcion = descripcion,
                    fecha_inicio,
                    fecha_fin,
                    hora,
                    minuto,
                    precio,
                    tipo,
                    profesor
                };

                FirebaseResponse response = await client.PushAsync("clases/", clase);
                MessageBox.Show("Clase creada exitosamente.");

                if (displayImage.Source != null)
                {
                    try
                    {
                        var stream = new MemoryStream();
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)displayImage.Source));
                        encoder.Save(stream);
                        stream.Position = 0;

                        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body);
                        string claseId = result["name"]; 
                        await client.UpdateAsync($"clases/{claseId}", new { id = claseId, clase});
                        var storageReference = firebaseStorage.Child("imagenesClases").Child($"{claseId}.png");
                        await storageReference.PutAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar la imagen: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir la clase a Firebase: " + ex.Message);
            }
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar imagen";
            openFileDialog.Filter = "Archivos de Imagen (*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                displayImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}