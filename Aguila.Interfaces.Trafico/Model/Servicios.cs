using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class Servicios
    {
        [Key]
        public int ServID { get; set; }

        public byte EmprID { get; set; }

        public string ServCodigo { get; set; }

        public string ServNombre { get; set; }

        public bool ServActiv { get; set; }

        public string ServTipo { get; set; }

        public decimal ServValor { get; set; }

        public string ServTipoIntervalo { get; set; }

    }
}
