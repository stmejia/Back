using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class invUbicacionBodegaQueryFilter
    {
        public int? idBodega { get; set; }
        public int? estante { get; set; }
        public int? pasillo { get; set; }
        public int? nivel { get; set; }
        public int? lugar { get; set; }
        public int? idProducto { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
