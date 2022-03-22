using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class RecursosAtributosQueryFilter
    {
        public string Nombre { get; set; }
        public int? RecursoId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
