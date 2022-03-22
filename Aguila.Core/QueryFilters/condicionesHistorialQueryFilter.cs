using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class condicionesHistorialQueryFilter
    {
        public int? idCondicion { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
