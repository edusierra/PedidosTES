using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ventas.Model
{
    [Keyless]
    public class TotalDepto
    {
        public string Codigo { get; set; }
        public string Departamento { get; set; }
        public decimal Total { get; set; }
    }
}
