using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class EstacionesTrabajoQueryFilter
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public bool? Activa { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
