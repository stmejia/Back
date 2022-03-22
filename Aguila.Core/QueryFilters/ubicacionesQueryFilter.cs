using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class ubicacionesQueryFilter
    {
        public int? idMunicipio { get; set; }
        public byte? idEmpresa { get; set; }
        public string codigo { get; set; }
        public Boolean? esPuerto { get; set; }
        public string lugar { get; set; }
        public string codigoPostal { get; set; }
        public decimal? latitud { get; set; }
        public decimal? longitud { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
