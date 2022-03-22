using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class paisesQueryFilter
    {
        public string Nombre { get; set; }
        public string CodMoneda { get; set; }
        public string CodAlfa2 { get; set; }
        public string CodAlfa3 { get; set; }
        public int? CodNumerico { get; set; }
        public string Idioma { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
