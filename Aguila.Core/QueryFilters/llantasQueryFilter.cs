using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class llantasQueryFilter
    {
        public int? idEstadoIngreso { get; set; }
        public int? proveedorId { get; set; }
        public int? idLlantaTipo { get; set; }
        public string codigo { get; set; }
        public string marca { get; set; }
        public string serie { get; set; }
        public string reencaucheIngreso { get; set; }
        public string medidas { get; set; }
        public decimal? precio { get; set; }
        public bool? llantaDoble { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime? fechaBaja { get; set; }
        public string propositoIngreso { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
