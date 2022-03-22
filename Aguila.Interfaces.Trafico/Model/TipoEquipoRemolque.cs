using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class TipoEquipoRemolque
    {
        [Key]
        public short TpEqID { get; set; }

        public byte EmprID { get; set; }

        public string TpEqCodigo { get; set; }

        public string TpEqNombre { get; set; }

        public short? TpEqEjes { get; set; }

        public short? TpEqTamaño { get; set; }

        public short? TpEqExtensibleA { get; set; }

        public string TpEqForma { get; set; }

    }
}
