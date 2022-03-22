using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class municipiosQueryFilter
    {
        public int? idDepartamento { get; set; }
        public string codMunicipio { get; set; }
        public string nombreMunicipio { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
