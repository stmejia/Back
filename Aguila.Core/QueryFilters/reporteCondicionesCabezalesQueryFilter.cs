using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class reporteCondicionesCabezalesQueryFilter
    {
        public byte? idEmpresa { get; set; }
        public int? idEstado { get; set; }
        public string movimiento { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string? listaIdEstados { get; set; }
        public string? codigo { get; set; }

        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
    }
}
