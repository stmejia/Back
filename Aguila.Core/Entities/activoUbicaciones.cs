using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class activoUbicaciones
    {
        public int id { get; set; }
        public int idActivo { get; set; }
        public int idUbicacion { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual activoOperaciones activoOperacion { get; set; }
        public virtual ubicaciones ubicacion { get; set; }
    }
}
