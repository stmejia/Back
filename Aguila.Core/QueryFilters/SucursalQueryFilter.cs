using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class SucursalQueryFilter
    {
        public string Nombre { get; set; }
        public bool? Activa { get; set; }
        public byte? EmpresaId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
