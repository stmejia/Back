using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class tipoProveedoresDto
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string descripcion { get; set; }

        public DateTime fechaCreacion { get; set; }
    }
}
