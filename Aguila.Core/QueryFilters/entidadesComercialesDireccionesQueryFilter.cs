using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class entidadesComercialesDireccionesQueryFilter
    {
        public long? idEntidadComercial { get; set; }
        public long? idDireccion { get; set; }
        public string descripcion { get; set; }
       // public virtual string vDireccion { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
