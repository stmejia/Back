using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoMovimientosQueryFilter
    {
        public int? id { get; set; }
        public int? idActivo { get; set; }
        public int? ubicacionId { get; set; }
        public int? idEstado { get; set; }
        public int? idServicio { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public int? idEmpleado { get; set; }
        public long? idUsuario { get; set; }
        public string lugar { get; set; }
        public DateTime? fecha { get; set; }
        public byte? idEmpresa { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
