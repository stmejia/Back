using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class clientesDto
    {
        //public clientesDto()
        //{
        //    this.entidadComercial = new HashSet<entidadComercialDto>();
        //    this.direccion = new HashSet<direccionesDto>();
        //    this.direccionFiscal = new HashSet<direccionesDto>();
        //}

        public int id { get; set; }
        public int idTipoCliente { get; set; }
        public long idDireccion { get; set; }
        public long idEntidadComercial { get; set; }
        public string codigo { get; set; }
        //public int idCorporacion { get; set; }
        public byte idEmpresa { get; set; }
        public int diasCredito { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime fechaCreacion { get; set; }

        //public virtual ICollection<entidadComercialDto> entidadComercial { get; set; }
        //public virtual ICollection<direccionesDto> direccion { get; set; }
        //public virtual ICollection<direccionesDto> direccionFiscal { get; set; }

        public virtual entidadComercialDto entidadComercial { get; set; }
        public virtual direccionesDto direccion { get; set; }
        public virtual direccionesDto direccionFiscal { get; set; }

        public virtual tipoClientesDto tipoCliente { get; set; }
        public virtual corporacionesDto corporacion { get; set; }

        public virtual string vDireccion { get; set; }
        public virtual string vDireccionFiscal { get; set; }
    }
}
