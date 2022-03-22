using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class clienteTarifasDto
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idTarifa { get; set; }
        public decimal precio { get; set; }
        public bool activa { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual clientesDto cliente { get; set; }
        public virtual tarifarioDto tarifa { get; set; }
    }
}
