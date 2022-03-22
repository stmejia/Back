using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class pilotosDto
    {
        public int id { get; set; }
        public int idTipoPilotos { get; set; }
        public int idEmpleado { get; set; }
        public string codigoPiloto { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public DateTime? fechaBaja { get; set; }

        public virtual string vNombreEmpleado { get; set; }
        public virtual pilotosTiposDto tipoPiloto { get; set; }
    }
}
