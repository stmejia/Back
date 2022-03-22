using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class mecanicosDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public int idTipoMecanico { get; set; }
        public int idEmpleado { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual string vNombreEmpleado { get; set; }
        public virtual tipoMecanicosDto tipoMecanico { get; set; }
    }
}
