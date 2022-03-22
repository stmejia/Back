using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class tipoActivosDto
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string nombre { get; set; }

        public string area { get; set; }

        public bool operaciones { get; set; }

        public int? idCuenta { get; set; }

        public decimal porcentajeDepreciacionAnual { get; set; }

        public DateTime fechaCreacion { get; set; }
    }
}
