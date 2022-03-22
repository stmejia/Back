using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
   public class tiposListaQueryFilter
    {
        public int? idRecurso { get; set; }

        public string tipoDato { get; set; }

        public string campo { get; set; }

        public byte? idEmpresa { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
