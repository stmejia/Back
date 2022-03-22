using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public partial class UsuariosRoles
    {
        public long usuario_id { get; set; }
        public int rol_id { get; set; }

        public virtual Usuarios Usuario { get; set; }
        public virtual Roles Rol { get; set; }
    }
}
