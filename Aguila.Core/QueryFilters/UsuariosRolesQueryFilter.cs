using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class UsuariosRolesQueryFilter
    {
        public long? usuario_id { get; set; }
        public int? rol_id { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
