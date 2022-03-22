using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class AsigUsuariosRecursosAtributos
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public int EstacionTrabajoId { get; set; }
        public byte ModuloId { get; set; }
        public int RecursoAtributosId { get; set; }

        public virtual EstacionesTrabajo EstacionTrabajo { get; set; }
        public virtual Modulos Modulo { get; set; }
        public virtual RecursosAtributos RecursoAtributos { get; set; }
    }
}
