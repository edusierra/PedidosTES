using System.Collections.Generic;
using Ventas.Data;

namespace Ventas.Model
{
    internal class ModeloPedido : Pedido
    {
        public ModeloPedido()
        {
            productos = new List<ItemPedido>();
        }
        public List<ItemPedido> productos { get; set; }
    }
}