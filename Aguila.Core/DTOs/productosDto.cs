using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class productosDto
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

        public virtual int idCategoria { get; set; }
        public virtual string descCategoria { get; set; }
        public virtual string descSubCategoria { get; set; }
        public virtual string nombreMedida { get; set; }
    }
}
