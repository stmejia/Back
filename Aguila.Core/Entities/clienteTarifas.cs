using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class clienteTarifas
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idTarifa { get; set; }
        public decimal precio { get; set; }
        public bool activa { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual clientes cliente { get; set; }
        public virtual tarifario tarifa { get; set; }
    }
}
