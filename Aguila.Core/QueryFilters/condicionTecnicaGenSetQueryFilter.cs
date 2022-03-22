using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class condicionTecnicaGenSetQueryFilter
    {
        public long idCondicionActivo { get; set; }
        public string bateriaCodigo { get; set; }
        public bool? bateriaNivelAcido { get; set; }
        public bool? bateriaArnes { get; set; }
        public bool? bateriaTerminales { get; set; }
        public bool? bateriaGolpes { get; set; }
        public bool? bateriaCarga { get; set; }
        public bool? combustibleDiesel { get; set; }
        public bool? combustibleAgua { get; set; }
        public bool? combustibleAceite { get; set; }
        public bool? combustibleFugas { get; set; }
        public bool? filtroAceite { get; set; }
        public bool? filtroDiesel { get; set; }
        public bool? bombaAguaEstado { get; set; }
        public bool? escapeAgujeros { get; set; }
        public bool? escapeDañado { get; set; }
        public bool? cojinetesEstado { get; set; }
        public bool? arranqueFuncionamiento { get; set; }
        public bool? fajaAlternador { get; set; }
        public bool? enfriamientoAire { get; set; }
        public bool? enfriamientoAgua { get; set; }
        public bool? cantidadGeneradaVolts { get; set; }

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
