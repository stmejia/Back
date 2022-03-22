using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public abstract  class Documento
    {
        public long id { get; set; }
        public DateTime fecha { get; set; }
        public string serie { get; set; }
        public long numero { get; set; }
        public int idUsuario { get; set; }
    }
}
