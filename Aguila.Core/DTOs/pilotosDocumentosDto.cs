using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class pilotosDocumentosDto
    {
        public int id { get; set; }
        public int idPiloto { get; set; }
        public string nombreDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public Guid? idImagenRecursoDocumentos { get; set; }
        public DateTime? fechaVigencia { get; set; }
        public DateTime? fechaCreacion { get; set; }
    }
}
