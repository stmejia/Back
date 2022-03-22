using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class ModulosMnuQueryFilter
    {
        public int? MenuIdPadre { get; set; }
        public string Descrip { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
