using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class AsigUsuariosModulosDto
    {
        public long UsuarioId { get; set; }
        public byte ModuloId { get; set; }

        public virtual ModulosDto Modulo { get; set; }
    }
}
