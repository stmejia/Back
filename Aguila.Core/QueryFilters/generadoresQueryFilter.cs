using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class generadoresQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idTipoGenerador { get; set; }
        public decimal? capacidadGalones { get; set; }
        public int? numeroCilindros { get; set; }
        public string marcaGenerador { get; set; }
        public string tipoInstalacion { get; set; }
        public string tipoEnfriamiento { get; set; }
        public string aptoParaCA { get; set; }

        public string codigo { get; set; }
        public byte? idEmpresa { get; set; }
        public int? idEstado { get; set; }
        public string flota { get; set; }
        public bool? propio { get; set; }
        public bool? equipoActivo { get; set; }
        public bool? global { get; set; }
        public int? idEstacionTrabajo { get; set; }

        public bool ignorarFechas { get; set; } = false;
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
