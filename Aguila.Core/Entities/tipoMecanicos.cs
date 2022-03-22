using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class tipoMecanicos
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string especialidad { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
