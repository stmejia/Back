using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class reporteMovimientosGeneradoresQueryFilter
    {
        //Global para Movimientos
        public byte idEmpresa { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public string? codigo { get; set; }
        public int? idActivo { get; set; }

        public string listaIdEstados { get; set; }
        public string flota { get; set; }
        public bool? propio { get; set; }
        public bool? equipoActivo { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public int idUsuario { get; set; }
        public string tipoDocumento { get; set; }
        public int? documento { get; set; }

        public int? idTipoGenerador { get; set; }
        public decimal? capacidadGalones { get; set; }
        public int? numeroCilindros { get; set; }
        public string marcaGenerador { get; set; }
        public string tipoInstalacion { get; set; }
        public string tipoEnfriamiento { get; set; }
    }
}
