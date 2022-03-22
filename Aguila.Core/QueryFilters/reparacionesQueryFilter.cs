using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class reparacionesQueryFilter
    {
        public string codigo { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public int? idCategoria { get; set; }

        public int? idTipoReparacion { get; set; }

        public byte?  idEmpresa { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
