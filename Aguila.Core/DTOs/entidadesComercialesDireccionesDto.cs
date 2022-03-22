using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class entidadesComercialesDireccionesDto
    {
        public long id { get; set; }
        public long idEntidadComercial { get; set; }
        public long idDireccion { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual direccionesDto direccion { get; set; }
        public virtual string vDireccion { get; set; }
    }
}
