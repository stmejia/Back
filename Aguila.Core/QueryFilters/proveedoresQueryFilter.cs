using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class proveedoresQueryFilter
    {
        public string codigo { get; set; }
        public long? idDireccion { get; set; }
        public int? idTipoProveedor { get; set; }
        public int? idCorporacion { get; set; }
        public long? idEntidadComercial { get; set; }
        public byte? idEmpresa { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string nit { get; set; }
    }
}
