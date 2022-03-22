using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class clienteTarifasQueryFilter
    {
        public int? idCliente { get; set; }
        public int? idTarifa { get; set; }
        public decimal? precio { get; set; }
        public bool? activa { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
