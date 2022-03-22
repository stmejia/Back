using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class controlEquipoAjenoQueryFilter
    {
        public string nombrePiloto { get; set; }
        public string placaCabezal { get; set; }
        public DateTime? ingreso { get; set; }
        public DateTime? salida { get; set; }
        public long? idUsuario { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string empresa { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
