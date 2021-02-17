using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Item
    {
        public string Numpedido { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Subtotal { get; set; }

        public virtual Pedido NumpedidoNavigation { get; set; }
        public virtual Producto ProductoNavigation { get; set; }
    }
}
