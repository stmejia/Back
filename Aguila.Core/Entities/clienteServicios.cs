using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class clienteServicios
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idServicio { get; set; }
        public decimal precio { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual clientes cliente { get; set; }
        public virtual servicios servicio { get; set; }
    }
}
