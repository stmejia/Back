using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoUbicacionesQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idUbicacion { get; set; }
        public string observaciones { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
