using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs
{
    public class equipoGaritaDto
    {
        public int id { get; set; }
        public int tipoEquipo { get; set; }
        public string codigo { get; set; }
        public string tamanoEquipo { get; set; }
        public bool propio { get; set; }
        public int? idActivo { get; set; }

        public virtual activoOperacionesDto activoOperacion { get; set; }
    }
}
