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
using Ventas.Code;

namespace Ventas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            Business bl = new Business();
            DateTime? inicial = dpInicial.SelectedDate;
            DateTime? final = dpFinal.SelectedDate;
            if (inicial == null || final == null || inicial > final)
            {
                MessageBox.Show("Las fechas no son válidas");
            }
            else
            {
                var data = bl.TotalDepartamento((DateTime)inicial, (DateTime)final);
                grResultado.ItemsSource = data;
            }
        }

        private void btnComision_Click(object sender, RoutedEventArgs e)
        {
            Business bl = new Business();
            DateTime? inicial = dpInicial.SelectedDate;
            if(inicial != null)
            {
                var data = bl.CalcularComision((DateTime)inicial);
                grResultado.ItemsSource = data;
            }
            else
            {
                MessageBox.Show("Seleccione el año y mes requeridos");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NuevoPedido pedido = new NuevoPedido();
            pedido.ShowDialog();
        }
    }
}
