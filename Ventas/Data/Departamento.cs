using System;
using System.Collections.Generic;

#nullable disable

namespace Ventas.Data
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ciudads = new HashSet<Ciudad>();
        }

        public string Coddep { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Ciudad> Ciudads { get; set; }
    }
}
