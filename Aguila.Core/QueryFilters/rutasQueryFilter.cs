using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class rutasQueryFilter
    {
        public byte? idEmpresa { get; set; }
        public int? idUbicacionOrigen { get; set; }
        public int? idUbicacionDestino { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool? existeRutaAlterna { get; set; }
        public decimal? distanciaKms { get; set; }
        public string gradoPeligrosidad { get; set; }
        public string estadoCarretera { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
