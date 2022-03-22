using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class mecanicos
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public int idTipoMecanico { get; set; }
        public int idEmpleado { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual tipoMecanicos tipoMecanico { get; set; }
        public virtual empleados empleado { get; set; }
    }
}
