using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class condicionGenSet
    {
        public long idCondicionActivo { get; set; }
        public decimal? galonesRequeridos { get; set; }
        public decimal? galonesGenSet { get; set; }
        public decimal? galonesCompletar { get; set; }
        public decimal? horometro { get; set; }
        public DateTime? horaEncendida { get; set; }
        public DateTime? horaApagada { get; set; }
        public decimal? dieselEntradaSalida { get; set; }
        public decimal? dieselConsumido { get; set; }
        public decimal? horasTrabajadas { get; set; }
        public bool estExPuertasGolpeadas { get; set; }
        public bool estExPuertasQuebradas { get; set; }
        public bool estExPuertasFaltantes { get; set; }
        public bool estExPuertasSueltas { get; set; }
        public bool estExBisagrasQuebradas { get; set; }
        public bool panelGolpes { get; set; }
        public bool panelTornillosFaltantes { get; set; }
        public bool panelOtros { get; set; }
        public bool soporteGolpes { get; set; }
        public bool soporteTornillosFaltantes { get; set; }
        public bool soporteMarcoQuebrado { get; set; }
        public bool soporteMarcoFlojo { get; set; }
        public bool soporteBisagrasQuebradas { get; set; }
        public bool soporteSoldaduraEstado { get; set; }
        public bool revIntCablesQuemados { get; set; }
        public bool revIntCablesSueltos { get; set; }
        public bool revIntReparacionesImpropias { get; set; }
        public bool tanqueAgujeros { get; set; }
        public bool tanqueSoporteDanado { get; set; }
        public bool tanqueMedidorDiesel { get; set; }
        public bool tanqueCodoQuebrado { get; set; }
        public bool tanqueTapon { get; set; }
        public bool tanqueTuberia { get; set; }
        public bool pFaltMedidorAceite { get; set; }
        public bool pFaltTapaAceite { get; set; }
        public bool pFaltTaponRadiador { get; set; }

        public condicionActivos condicionActivo { get; set; }
    }
}
