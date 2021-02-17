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
using Ventas.Code;
using Ventas.Data;
using Ventas.Model;

namespace Ventas
{
    /// <summary>
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class NuevoPedido : Window
    {
        Business bl = new Business();
        List<Vendedor> vendedors;
        ModeloPedido model = new ModeloPedido();

        public NuevoPedido()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            cboDepto.ItemsSource = bl.ListaDepartamentos();
            vendedors = bl.ListarVendedores();
            cboVendedor.ItemsSource = vendedors;
            grPedido.ItemsSource = model.productos;
            cboProducto.ItemsSource = bl.ListarProductos();
        }

        private void cboDepto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboCiudad.ItemsSource = bl.ListarCiudades(((Departamento)cboDepto.SelectedValue).Coddep);
        }

        private void cboCiudad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboCiudad.SelectedIndex != -1)
                cboCliente.ItemsSource = bl.ListarClientes(((Ciudad)cboCiudad.SelectedValue).Codciu);
        }

        private void cboCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCliente.SelectedIndex != -1)
            {
                var cliente = (Cliente)cboCliente.SelectedValue;
                model.Cliente = cliente.Codcli;
                model.Vendedor = cliente.Vendedor;
                var item = vendedors.Where(x => x.Codvend == cliente.Vendedor).FirstOrDefault();
                if (item != null)
                    cboVendedor.SelectedValue = item;
            }
        }

        private void cboProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboProducto.SelectedIndex != -1)
            {
                var item = (Producto)cboProducto.SelectedValue;
                pedCantidad.Text = "1";
                pedPrecio.Text = item.Precio.ToString();
                CalcSubtotal();
            }
        }

        private void pedPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSubtotal();
        }

        private void pedCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSubtotal();
        }

        private void CalcSubtotal()
        {
            if (pedCantidad != null && pedPrecio != null && pedSubtotal != null)
            {
                if (pedCantidad.Text.Length > 0 && pedPrecio.Text.Length > 0)
                {
                    var total = decimal.Parse(pedCantidad.Text) * decimal.Parse(pedPrecio.Text);
                    pedSubtotal.Text = total.ToString();
                }
                else
                {
                    MessageBox.Show("Verifique el precio y/o la cantidad");
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Producto prod = (Producto)cboProducto.SelectedItem;
            var item = new ItemPedido()
            {
                CodProd = prod.Codprod,
                Producto = prod.Nombre,
                Cantidad = decimal.Parse(pedCantidad.Text),
                Precio = decimal.Parse(pedPrecio.Text),
            };
            item.Subtotal = item.Cantidad * item.Precio;
            model.productos.Add(item);
            grPedido.ItemsSource = null;
            grPedido.ItemsSource = model.productos;
            pedCantidad.Text = "1";
            pedPrecio.Text = "0";
            cboProducto.SelectedIndex = -1;
            var total = model.productos.Sum(x => x.Subtotal);
            pedTotal.Text = total.ToString();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            model.Fecha = dtFecha.SelectedDate;
            if (model.Fecha != null && !string.IsNullOrEmpty(model.Cliente) && model.productos.Count > 0)
            {
                if (bl.GuardarPedido(model))
                {
                    MessageBox.Show("Pedido guardado!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error guardando el pedido");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar los datos del pedido");
            }
        }
    }
}
