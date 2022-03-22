using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class transportes
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public long idProveedor { get; set; }
        public bool propio { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual proveedores proveedor { get; set; }
    }
}
