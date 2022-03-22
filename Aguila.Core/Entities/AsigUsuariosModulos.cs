using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class AsigUsuariosModulos
    {
        public long UsuarioId { get; set; }
        public byte ModuloId { get; set; }

        public virtual Modulos Modulo { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
