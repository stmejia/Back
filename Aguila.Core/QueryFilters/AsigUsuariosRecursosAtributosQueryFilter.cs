using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class AsigUsuariosRecursosAtributosQueryFilter
    {
        public long? UsuarioId { get; set; }
        public int? EstacionTrabajoId { get; set; }
        public byte? ModuloId { get; set; }
        public int? RecursoAtributosId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

