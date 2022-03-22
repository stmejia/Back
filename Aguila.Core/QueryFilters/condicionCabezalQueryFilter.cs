using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class condicionCabezalQueryFilter
    {
        public long? idCondicionActivo { get; set; }
        public int? idEstado { get; set; }
        public int? idReparacion { get; set; }
        public int? idPiloto { get; set; }
        public int? idUsuario { get; set; }
        public string windShield { get; set; }
        public string plumillas { get; set; }
        public string viscera { get; set; }
        public string rompeVientos { get; set; }
        public string persiana { get; set; }
        public string bumper { get; set; }
        public string capo { get; set; }
        public string retrovisor { get; set; }
        public string ojoBuey { get; set; }
        public string pataGallo { get; set; }
        public string portaLlanta { get; set; }
        public string spoilers { get; set; }
        public string salpicadera { get; set; }
        public string guardaFango { get; set; }
        public string taponCombustible { get; set; }
        public string baterias { get; set; }
        public string lucesDelanteras { get; set; }
        public string lucesTraseras { get; set; }
        public string pintura { get; set; }
        public string llanta1 { get; set; }
        public string llanta2 { get; set; }
        public string llanta3 { get; set; }
        public string llanta4 { get; set; }
        public string llanta5 { get; set; }
        public string llanta6 { get; set; }
        public string llanta7 { get; set; }
        public string llanta8 { get; set; }
        public string llanta9 { get; set; }
        public string llanta10 { get; set; }
        public string llantaR { get; set; }
        public string irregularidadesObserv { get; set; }

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
