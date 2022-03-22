using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class entidadComercial
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string razonSocial { get; set; }
        public int? idCorporacion { get; set; }
        public long? idDireccionFiscal { get; set; }
        //public string tipo { get; set; }
        public string nit { get; set; }
        public string tipoNit { get; set; }
        public DateTime fechaCreacion { get; set; }

       // public virtual corporaciones corporacion { get; set; }
    }
}
