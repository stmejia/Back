using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class clienteServiciosDto
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idServicio { get; set; }
        public decimal precio { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
