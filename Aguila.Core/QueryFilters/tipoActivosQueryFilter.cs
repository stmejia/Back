using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class tipoActivosQueryFilter
    {
        public string codigo { get; set; }

        public string nombre { get; set; }

        public string area { get; set; }

        public bool? operaciones { get; set; }

        public int? idCuenta { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
