using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class tipoClientes
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public bool naviera { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
