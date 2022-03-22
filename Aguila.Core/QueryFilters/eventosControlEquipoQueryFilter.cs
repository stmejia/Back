using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class eventosControlEquipoQueryFilter
    {
        public int? idActivo { get; set; }
        public long? idUsuarioCreacion { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string descripcionEvento { get; set; }
        public long? idUsuarioRevisa { get; set; }
        public long? idUsuarioResuelve { get; set; }
        public long? idUsuarioAnula { get; set; }
        public string estado { get; set; }
        public string categoria { get; set; }
        public string tipoActivo { get; set; }

        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
