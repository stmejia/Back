using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class serviciosQueryFilter
    {
        public byte? idEmpresa { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal? precio { get; set; }
        public bool? ruta { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
