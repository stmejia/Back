using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class activoGenerales
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string descripcion { get; set; }

        public DateTime fechaCompra { get; set; }

        public decimal valorCompra { get; set; }

        public decimal valorLibro { get; set; }

        public decimal valorRescate { get; set; }

        public DateTime? fechaBaja { get; set; }

        public decimal depreciacionAcumulada { get; set; }

        public long idDocumentoCompra { get; set; }

        public int idTipoActivo { get; set; }

        public string tituloPropiedad { get; set; }

        public string polizaImportacion { get; set; }

        public DateTime fechaCreacion { get; set; }

        public virtual tipoActivos tipoActivo { get; set; }
    }
}
