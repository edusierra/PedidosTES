using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Clientes = new HashSet<Cliente>();
        }

        public string Codciu { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }

        public virtual Departamento DepartamentoNavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
