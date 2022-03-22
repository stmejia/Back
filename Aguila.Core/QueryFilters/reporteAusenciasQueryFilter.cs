using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class reporteAusenciasQueryFilter
    {
        public string cui { get; set; }
        public string evento { get; set; }
        public DateTime? fecha { get; set; }

        public int? idEstacionTrabajo { get; set; }
        public string localidad { get; set; }
        public byte? idEmpresa { get; set; }

    }
}
