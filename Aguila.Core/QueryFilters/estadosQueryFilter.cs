using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class estadosQueryFilter
    {
        public byte? idEmpresa { get; set; }
        public string codigo { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public int? numeroOrden { get; set; }
        public bool? disponible { get; set; }
        public string evento { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
