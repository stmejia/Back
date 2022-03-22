using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class productos
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string codigoQR { get; set; }
        public string descripcion { get; set; }
        public string bienServicio { get; set; }
        public int idsubCategoria { get; set; }
        public int idMedida { get; set; }
        public byte idEmpresa { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual invSubCategoria subCategoria { get; set; }
        public virtual Empresas empresa { get; set; }
        public virtual medidas medida { get; set; }
    }
}
