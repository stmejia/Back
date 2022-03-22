using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoMovimientosActualQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idEstado { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public int? idServicio { get; set; }
        public int? idEmpleado { get; set; }
        public int? idRuta { get; set; }
        public int? idUsuario { get; set; }
        public int? documento { get; set; }
        public string tipoDocumento { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
