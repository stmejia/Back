using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class activoEstados
    {
        public int id { get; set; }
        public int idActivo { get; set; }
        public int idEstado { get; set; }
        public string observacion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual activoOperaciones activoOperacion { get; set; }
        public virtual estados estado { get; set; }
    }
}
