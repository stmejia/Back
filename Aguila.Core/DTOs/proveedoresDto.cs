using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class proveedoresDto
    {
        //public proveedoresDto()
        //{
        //    this.entidadComercial = new HashSet<entidadComercialDto>();
        //   // this.direccion = new HashSet<direccionesDto>();
        //    this.direccionFiscal = new HashSet<direccionesDto>();
        //}
        public long id { get; set; }
        public string codigo { get; set; }
        public long idDireccion { get; set; }
        public int idTipoProveedor { get; set; }
        //public int idCorporacion { get; set; }
        public long idEntidadComercial { get; set; }
        public byte idEmpresa { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual entidadComercialDto entidadComercial { get; set; }
        public virtual direccionesDto direccion { get; set; }
        public virtual direccionesDto direccionFiscal { get; set; }


        public virtual tipoProveedoresDto tipoProveedor { get; set; }
        public virtual corporacionesDto corporacion { get; set; }

        public virtual string vDireccion { get; set; }
        public virtual string vDireccionFiscal { get; set; }
    }
}
