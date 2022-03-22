using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class tarifarioQueryFilter
    {
        public string codigo { get; set; }
        public string tipoCarga { get; set; }
        public string tipoMovimiento { get; set; }
        public string segmento { get; set; }
        public int? idUbicacionOrigen { get; set; }
        public int? idUbicacionDestino { get; set; }
        public int? idRuta { get; set; }
        public int? idServicio { get; set; }
        public byte? idEmpresa { get; set; }
        public decimal? combustibleGls { get; set; }
        public decimal? precio { get; set; }
        public decimal? kmRecorridosCargado { get; set; }
        public decimal? kmRecorridosVacio { get; set; }
        public bool? esEspecializado { get; set; }
        public string tipoViaje { get; set; }
        public DateTime fechaVigencia { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
