namespace Ventas.Model
{
    internal class ItemPedido
    {
        public string CodProd { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal {get;set;}
    }
}