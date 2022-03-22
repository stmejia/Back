using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class EmpresaQueryFilter
    {
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public bool? esEmpleador { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
