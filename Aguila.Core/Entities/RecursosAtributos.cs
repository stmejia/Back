using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class RecursosAtributos
    {
        public int Id { get; set; }
        public byte Codigo { get; set; }
        public string Nombre { get; set; }
        public int RecursoId { get; set; }

        public virtual Recursos Recurso { get; set; }
    }
}
