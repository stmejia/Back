using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class tipoMecanicosQueryFilter
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string especialidad { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
