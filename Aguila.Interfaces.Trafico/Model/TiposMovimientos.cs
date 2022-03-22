using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class TiposMovimientos
    {
        [Key]
        public short TpMvID { get; set; }

        public string TpMvCodigo { get; set; }

        public string TpMvNombre { get; set; }

        public short EmprID { get; set; }

    }

}
