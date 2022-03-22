using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionFurgonDto
    {
        public long idCondicionActivo { get; set; }
        public bool revExtGolpe { get; set; }
        public bool revExtSeparacion { get; set; }
        public bool revExtRoturas { get; set; }
        public bool revIntGolpes { get; set; }
        public bool revIntSeparacion { get; set; }
        public bool revIntFiltra { get; set; }
        public bool revIntRotura { get; set; }
        public bool revIntPisoH { get; set; }
        public bool revIntManchas { get; set; }
        public bool revIntOlores { get; set; }
        public string revPuertaCerrado { get; set; }
        public string revPuertaEmpaque { get; set; }
        public string revPuertaCinta { get; set; }
        public bool limpPiso { get; set; }
        public bool limpTecho { get; set; }
        public bool limpLateral { get; set; }
        public bool limpExt { get; set; }
        public bool limpPuerta { get; set; }
        public bool limpMancha { get; set; }
        public bool limpOlor { get; set; }
        public bool limpRefuerzo { get; set; }
        public bool lucesA { get; set; }
        public bool lucesB { get; set; }
        public bool lucesC { get; set; }
        public bool lucesD { get; set; }
        public bool lucesE { get; set; }
        public bool lucesF { get; set; }
        public bool lucesG { get; set; }
        public bool lucesH { get; set; }
        public bool lucesI { get; set; }
        public bool lucesJ { get; set; }
        public bool lucesK { get; set; }
        public bool lucesL { get; set; }
        public bool lucesM { get; set; }
        public bool lucesN { get; set; }
        public bool lucesO { get; set; }
        public bool guardaFangosI { get; set; }
        public bool guardaFangosD { get; set; }
        public string fricciones { get; set; }
        public string senalizacion { get; set; }
        public bool placaPatin { get; set; }
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
        public string llanta11 { get; set; }
        public string llantaR { get; set; }
        public string llantaR2 { get; set; }

        public virtual condicionActivosDto condicionActivo { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantas { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantasRepuesto { get; set; }
    }
}
