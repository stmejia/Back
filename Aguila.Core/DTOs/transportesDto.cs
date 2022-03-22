using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class transportesDto
    {
        public transportesDto()
        {
            this.proveedores = new HashSet<proveedoresDto>();
            this.entidadComercial = new HashSet<entidadComercialDto>();
        }

        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public long idProveedor { get; set; }
        public bool propio { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual ICollection<proveedoresDto> proveedores { get; set; }
        public virtual ICollection<entidadComercialDto> entidadComercial { get; set; }
    }
}
