using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class tipoReparaciones
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public byte idEmpresa { get; set; }

        public DateTime fechaCreacion { get; set; }
        public virtual Empresas empresa { get; set; }
    }
}
