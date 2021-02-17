using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Producto
    {
        public Producto()
        {
            Items = new HashSet<Item>();
        }

        public string Codprod { get; set; }
        public string Nombre { get; set; }
        public string Familia { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
