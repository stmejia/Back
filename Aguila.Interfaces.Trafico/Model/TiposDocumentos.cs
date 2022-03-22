using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class TiposDocumentos
    {
        [Key]
        public short TpDcID { get; set; }

        public string TpDcCodigo { get; set; }

        public string TpDcNombre { get; set; }

        public bool TpDcSerie2 { get; set; }

        public byte EmprID { get; set; }

        public string TpDcValidacionForm { get; set; }

        public bool TpDcMovimiento { get; set; }

        public bool TpDcActiv { get; set; }

        public bool TpDcPropios { get; set; }

        public bool TpDcAgregados { get; set; }

        public string FileImpresion { get; set; }

    }
}
