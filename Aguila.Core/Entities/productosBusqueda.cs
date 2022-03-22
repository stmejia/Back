using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class productosBusqueda
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public int idProducto { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual productos producto { get; set; }
    }
}
