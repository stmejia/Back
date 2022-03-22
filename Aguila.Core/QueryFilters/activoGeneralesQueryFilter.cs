using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoGeneralesQueryFilter
    {
        public string codigo { get; set; }
        public DateTime? fechaCompra { get; set; }
        public DateTime? fechaBaja { get; set; }
        public long? idDocumentoCompra { get; set; }
        public int? idTipoActivo { get; set; }
        public string polizaImportacion { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
