using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class controlVisitasQueryFilter
    {
        
        public string nombre { get; set; }
        public string identificacion { get; set; }
        public string motivoVisita { get; set; }
        public string areaVisita { get; set; }
        public string nombreQuienVisita { get; set; }
        public string vehiculo { get; set; }
        public DateTime? ingreso { get; set; }
        public DateTime? salida { get; set; }
        public long? idUsuario { get; set; }
        public string fechaCreacion { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string empresaVisita { get; set; }
        public Boolean? enPredio { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
