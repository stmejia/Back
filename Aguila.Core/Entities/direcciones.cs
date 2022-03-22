using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class direcciones
    {
        public long id { get; set; }
        public int idMunicipio { get; set; }
        public string colonia { get; set; }
        public string zona { get; set; }
        public string codigoPostal { get; set; }
        public string direccion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual municipios municipio { get; set; }
    }
}
