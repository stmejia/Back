using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class condicionTallerVehiculoQueryFilter
    {
        public int? id { get; set; }
        public int? idActivo { get; set; }
        public int? idEmpleado { get; set; }
        public long? idUsuario { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string serie { get; set; }
        public int? numero { get; set; }
        public string vidrios { get; set; }
        public string llantas { get; set; }
        public string tanqueCombustible { get; set; }
        public string observaciones { get; set; }
        public DateTime? fechaAprobacion { get; set; }
        public DateTime? fechaRechazo { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime? fechaSalida { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
