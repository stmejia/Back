using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class AsigUsuariosEstacionesTrabajoDto
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public int EstacionTrabajoId { get; set; }

        public virtual EstacionesTrabajoDto EstacionTrabajo { get; set; }
    }
}
