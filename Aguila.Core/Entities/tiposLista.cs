using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public partial class tiposLista
    {
        public int id { get; set; }

        public string descripcion { get; set; }

        public int idRecurso { get; set; }

        public string tipoDato { get; set; }

        public string campo { get; set; }

        public DateTime fechaCreacion { get; set; }

        public virtual Recursos recurso { get; set; }

    }
}
