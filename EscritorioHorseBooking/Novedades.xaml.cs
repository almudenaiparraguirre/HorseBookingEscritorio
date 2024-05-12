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
        FirebaseStorage firebaseStorage;

        public Novedades()
        {
            InitializeComponent();

            client = new FireSharp.FirebaseClient(config);
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

        private async void crearNovedad_Click(object sender, RoutedEventArgs e)
        {
            string tituloNovedad = textBoxtituloNoticia.Text;
            string descripcion = textBoxdescNoticia.Text;

            FirebaseResponse response = await client.PushAsync("novedades/", new { titulo = tituloNovedad, descripcion = descripcion, fecha = DateTime.Now });
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body);
            string novedadId = result["name"];
            await client.UpdateAsync($"novedades/{novedadId}", new { id = novedadId, titulo = tituloNovedad, descripcion = descripcion, fecha = DateTime.Now });

            if (displayImage.Source != null)
            {
                try
                {
                    var stream = new MemoryStream();
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)displayImage.Source));
                    encoder.Save(stream);
                    stream.Position = 0;

                    var storageReference = firebaseStorage.Child("imagenesNovedades").Child($"{novedadId}.png");
                    await storageReference.PutAsync(stream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar la imagen: {ex.Message}");
                }
            }
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
