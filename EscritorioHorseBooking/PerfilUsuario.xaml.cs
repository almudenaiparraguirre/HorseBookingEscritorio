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
    /// Lógica de interacción para PerfilUsuario.xaml
    /// </summary>
    public partial class PerfilUsuario : Window
    {
        public PerfilUsuario()
        {
            InitializeComponent();
        }
        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            var pantalla = new PantallaInicial();
            pantalla.Show();
            this.Close();
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

        private void Clases_Click(object sender, RoutedEventArgs e)
        {
            var crearClase = new crearClase();
            crearClase.Show();
            this.Close();
        }
    }
}
