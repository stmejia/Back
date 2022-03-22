using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
   public class ModuloQueryFilter
    {
        public string nombre { get; set; }
        public bool? activo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
