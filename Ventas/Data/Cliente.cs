using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            InversePadreNavigation = new HashSet<Cliente>();
            Pedidos = new HashSet<Pedido>();
        }

        public string Codcli { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int? Cupo { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Canal { get; set; }
        public string Vendedor { get; set; }
        public string Ciudad { get; set; }
        public string Padre { get; set; }

        public virtual Ciudad CiudadNavigation { get; set; }
        public virtual Cliente PadreNavigation { get; set; }
        public virtual Vendedor VendedorNavigation { get; set; }
        public virtual ICollection<Cliente> InversePadreNavigation { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
