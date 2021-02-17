using Microsoft.EntityFrameworkCore;

namespace Ventas.Model
{
    [Keyless]
    public class ComisionVendedor
    {
        public string Codigo { get; set; }
        public string Vendedor { get; set; }
        public decimal Total { get; set; }
        public decimal Comision { get; set; }
    }
}