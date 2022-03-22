using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class entidadesComercialesDirecciones
    {
        public long id { get; set; }
        public long idEntidadComercial { get; set; }
        public long idDireccion { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual entidadComercial entidadComercial { get; set; }
        public virtual direcciones direccion { get; set; }
        //public virtual string vDireccion { get; set; }
    }
}
