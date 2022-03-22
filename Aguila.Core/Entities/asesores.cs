using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class asesores
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int idEmpleado { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual empleados empleado { get; set; }
    }
}
