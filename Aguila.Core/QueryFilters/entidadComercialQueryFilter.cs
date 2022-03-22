using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class entidadComercialQueryFilter
    {
        public string nombre { get; set; }
        public string razonSocial { get; set; }
        public long? idDireccionFiscal { get; set; }
        public string tipo { get; set; }
        public byte idEmpresa { get; set; }
        public string nit { get; set; }
        public string tipoNit { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
