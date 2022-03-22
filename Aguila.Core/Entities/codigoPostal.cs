using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class codigoPostal
    {
        public int id { get; set; }
        public int idMunicipio { get; set; }
        public string codigo { get; set; }

        public virtual municipios municipio  { get; set; }
    }
}
