using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class invSubCategoria
    {
        public int id { get; set; }
        public int idInvCategoria { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual invCategoria invCategoria { get; set; }
    }
}
