using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class mecanicosQueryFilter
    {
        public string codigo { get; set; }
        public int? idTipoMecanico { get; set; }
        public int? idEmpleado { get; set; }
        public DateTime? fechaBaja { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
