using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class proveedores
    {
        public long id { get; set; }
        public string codigo { get; set; }
        public long idDireccion { get; set; }
        public int idTipoProveedor { get; set; }
        //public int idCorporacion { get; set; }
        public long idEntidadComercial { get; set; }
        public byte idEmpresa { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime? fechaCreacion { get; set; }

      
        public virtual direcciones direccion { get; set; }
        public virtual tipoProveedores tipoProveedor { get; set; }
        //public virtual corporaciones corporacion { get; set; }
        public virtual entidadComercial entidadComercial { get; set; }
        public virtual Empresas empresa { get; set; }
    }
}
