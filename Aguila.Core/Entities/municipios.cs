using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class municipios
    {
        public int id { get; set; }
        public int idDepartamento { get; set; }
        public string codMunicipio { get; set; }
        public string nombreMunicipio { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual departamentos departamento { get; set; }
    }
}
