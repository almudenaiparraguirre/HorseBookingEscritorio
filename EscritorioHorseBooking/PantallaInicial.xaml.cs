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
    /// Lógica de interacción para PantallaInicial.xaml
    /// </summary>
    public partial class PantallaInicial : Window
    {
        public PantallaInicial()
        {
            InitializeComponent();
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

        private void ResumenClases_Click(object sender, RoutedEventArgs e)
        {
            // Código para mostrar el resumen de las clases
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            // Código para abrir el chat
        }

        private void Clases_Click(object sender, RoutedEventArgs e)
        {
            // Código para mostrar la página de clases
        }
    }
}
