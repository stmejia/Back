using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class entidadComercialDto
    {
        public entidadComercialDto()
        {
            this.direccionFiscal = new direccionesDto();
        }
       
        public long id { get; set; }
        public string nombre { get; set; }
        public string razonSocial { get; set; }
        public int? idCorporacion { get; set; }
        public long? idDireccionFiscal { get; set; }       
        public string nit { get; set; }
        public string tipoNit { get; set; }
        public DateTime fechaCreacion { get; set; }
               
        public virtual direccionesDto direccionFiscal { get; set; }
        public virtual string tipo { get; set; }

    }
}
