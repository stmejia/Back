using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class AsigUsuariosEstacionesTrabajo
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public int EstacionTrabajoId { get; set; }
        public virtual EstacionesTrabajo EstacionTrabajo { get; set; }
        //public  Usuario Usuario { get; set; }
    }
}
