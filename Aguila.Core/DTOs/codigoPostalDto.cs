using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs
{
    public class codigoPostalDto
    {
        public int id { get; set; }
        public int idMunicipio { get; set; }
        public string codigo { get; set; }

        public virtual municipiosDto municipio { get; set; }
    }
}
