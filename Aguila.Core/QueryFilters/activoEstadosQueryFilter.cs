using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoEstadosQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idEstado { get; set; }
        public string observacion { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
