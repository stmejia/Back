using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
   public class listasQueryFilter
    {
        public string valor { get; set; }
        public string descripcion { get; set; }
        public byte? idEmpresa { get; set; }
        public int? idTipoLista { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
