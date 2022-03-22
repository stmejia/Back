using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class TipoContenedor
    {
        [Key]
        public short TpCnID { get; set; }

        public byte EmprID { get; set; }

        public string TpCnCodigo { get; set; }

        public short? TpCnTamaño { get; set; }

        public string TpCnISOType { get; set; }

        public string TpCnNombre { get; set; }

        public string TpCnAbrevia { get; set; }

        public bool TpCnRefrigerado { get; set; }

    }
}
