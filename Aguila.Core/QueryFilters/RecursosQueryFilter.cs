using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
   public class RecursosQueryFilter
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool? Activo { get; set; }
        public string Controlador { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
