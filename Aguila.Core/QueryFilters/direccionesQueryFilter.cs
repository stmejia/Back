using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class direccionesQueryFilter
    {
        public int? idMunicipio { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string zona { get; set; }
        public string codigoPostal { get; set; }
        public string direccion { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
