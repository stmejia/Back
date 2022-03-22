using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class llantaTiposQueryFilter
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        //public byte? proposito { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
