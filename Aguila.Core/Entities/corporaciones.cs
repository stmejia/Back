using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class corporaciones
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool propio { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
