using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Clientes = new HashSet<Cliente>();
            Pedidos = new HashSet<Pedido>();
        }

        public string Codvend { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
