using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class reparaciones
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public int idCategoria { get; set; }

        public int idTipoReparacion { get; set; }

        public byte idEmpresa { get; set; }

        public decimal? horasHombre { get; set; }

        public DateTime fechaCreacion { get; set; }

        public virtual tipoReparaciones tipo { get; set; }
        public virtual listas categoria { get; set; }
        public virtual Empresas empresa { get; set; }
    }
}
