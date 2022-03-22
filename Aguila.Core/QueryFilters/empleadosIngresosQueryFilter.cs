using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class empleadosIngresosQueryFilter
    {
        public string cui { get; set; }
        public string evento { get; set; }

        //para reporte de horas de ingresos y ausencias
        public string hora { get; set; }
        public Boolean? antesDe { get; set; }     
        //

        public string? vehiculo { get; set; }
        public int? idEstacionTrabajo { get; set; }
        

        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
