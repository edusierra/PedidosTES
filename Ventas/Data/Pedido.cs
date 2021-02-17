using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Pedido
    {
        public Pedido()
        {
            Items = new HashSet<Item>();
        }

        public string Numpedido { get; set; }
        public string Cliente { get; set; }
        public DateTime? Fecha { get; set; }
        public string Vendedor { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
        public virtual Vendedor VendedorNavigation { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
