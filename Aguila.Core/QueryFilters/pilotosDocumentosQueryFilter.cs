using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class pilotosDocumentosQueryFilter
    {
        public int? idPiloto { get; set; }
        public string nombreDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public Guid? idImagenRecursoDocumentos { get; set; }
        public DateTime? fechaVigencia { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
