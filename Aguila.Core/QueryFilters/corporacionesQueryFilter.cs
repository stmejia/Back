using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class corporacionesQueryFilter
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool? propio { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
