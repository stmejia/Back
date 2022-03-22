using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class estados
    {
        public int id { get; set; }
        public byte idEmpresa { get; set; }
        public string codigo { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public int numeroOrden { get; set; }
        public bool disponible { get; set; }
        public string evento { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual Empresas empresa { get; set; }
    }
}
