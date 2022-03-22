using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class AsigUsuariosModulosQueryFilter
    {
        public long? UsuarioId { get; set; }
        public byte? ModuloId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
