using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class AsigUsuariosRecursosAtributosDto
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public int EstacionTrabajoId { get; set; }
        public byte ModuloId { get; set; }
        public int RecursoAtributosId { get; set; }
    }
}
