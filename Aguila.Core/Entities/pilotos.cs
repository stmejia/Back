using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class pilotos
    {
        public int id { get; set; }
        public int idTipoPilotos { get; set; }
        public int idEmpleado { get; set; }
        public string codigoPiloto { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public DateTime? fechaBaja { get; set; }

        public virtual pilotosTipos pilotoTipo { get; set; }
        public virtual empleados empleado { get; set; }
    }
}
