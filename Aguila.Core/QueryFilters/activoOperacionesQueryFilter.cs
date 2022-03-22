using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class activoOperacionesQueryFilter
    {
        public string codigo { get; set; }
        public string codigoInit { get; set; }
        public string descripcion { get; set; }
        public DateTime? fechaBaja { get; set; }
        public string categoria { get; set; }
        public string color { get; set; }
        public string marca { get; set; }
        public string vin { get; set; }
        public int? correlativo { get; set; }
        public string serie { get; set; }
        public short? modeloAnio { get; set; }
        public int? idActivoGenerales { get; set; }
        public int? idTransporte { get; set; }
        public string flota { get; set; }

        public byte? idEmpresa { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
