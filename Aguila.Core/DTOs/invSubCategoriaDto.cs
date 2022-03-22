using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class invSubCategoriaDto
    {
        public int id { get; set; }
        public int idInvCategoria { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual EmpresasDto empresa { get; set; }
    }
}
