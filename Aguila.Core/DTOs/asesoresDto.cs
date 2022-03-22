using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class asesoresDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int idEmpleado { get; set; }
        public DateTime? fechaCreacion { get; set; }
    }
}
