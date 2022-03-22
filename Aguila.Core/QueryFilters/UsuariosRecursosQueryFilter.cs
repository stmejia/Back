using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class UsuariosRecursosQueryFilter
    {
        public int? estacionTrabajo_id { get; set; }
        public int? recurso_id { get; set; }
        public long? usuario_id { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
