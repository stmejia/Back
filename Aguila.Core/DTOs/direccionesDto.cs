using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class direccionesDto
    {
        public long id { get; set; }
        public int idMunicipio { get; set; }
        public string colonia { get; set; }
        public string zona { get; set; }
        public string codigoPostal { get; set; }
        public string direccion { get; set; }
        public DateTime? fechaCreacion { get; set; }

        //public virtual int idPais { get; set; }
        //public virtual int idDepartamento { get; set; }
        public virtual municipiosDto municipio { get; set; }
    }
}
