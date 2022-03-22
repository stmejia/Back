using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class departamentos
    {
        public int id { get; set; }
        public int idPais{ get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual paises pais { get; set; }
    }
}
