using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class UsuarioQueryFilter
    {
        public string nombre { get; set; }
        public DateTime? FchCreacion { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
  
}
