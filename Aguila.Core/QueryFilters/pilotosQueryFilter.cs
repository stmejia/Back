using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class pilotosQueryFilter
    {
        public int? idTipoPilotos { get; set; }
        public int? idEmpleado { get; set; }
        public string codigoPiloto { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public DateTime? fechaBaja { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
