using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class clienteServiciosQueryFilter
    {
        public int? idCliente { get; set; }
        public int? idServicio { get; set; }
        public decimal? precio { get; set; }
        public DateTime vigenciaHasta { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
