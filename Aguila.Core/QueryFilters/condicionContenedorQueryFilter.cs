using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class condicionContenedorQueryFilter
    {
        public long? idCondicionActivo { get; set; }
        public string tipoContenedor { get; set; }
        public bool? exteriorMarcos { get; set; }
        public string exteriorMarcosObs { get; set; }
        public bool? puertasInteriorExterior { get; set; }
        public string puertasInteriorExteriorObs { get; set; }
        public bool? pisoInterior { get; set; }
        public string pisoInteriorObs { get; set; }
        public bool? techoCubierta { get; set; }
        public string techoCubiertaObs { get; set; }
        public bool? ladosIzquierdoDerecho { get; set; }
        public string ladosIzquierdoDerechoObs { get; set; }
        public bool? paredFrontal { get; set; }
        public string paredFrontalObs { get; set; }
        public bool? areaCondensadorCompresor { get; set; }
        public string areaCondensadorCompresorObs { get; set; }
        public bool? areaEvaporador { get; set; }
        public string areaEvaporadorObs { get; set; }
        public bool? areaBateria { get; set; }
        public string areaBateriaObs { get; set; }
        public bool? cajaControlElectricoAutomatico { get; set; }
        public string cajaControlElectricoAutomaticoObs { get; set; }
        public bool? cablesConexionElectrica { get; set; }
        public string cablesConexionElectricaObs { get; set; }

        public string movimiento { get; set; }
        public bool? disponible { get; set; }
        public bool? cargado { get; set; }
        public bool? inspecVeriOrden { get; set; }
        public bool ignorarFechas { get; set; } = false;
        public int? idEstacionTrabajo { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
