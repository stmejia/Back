using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionEquipoDto
    {
        public long idCondicionActivo { get; set; }
        public bool lucesA { get; set; }
        public bool lucesB { get; set; }
        public bool lucesC { get; set; }
        public bool lucesD { get; set; }
        public bool lucesE { get; set; }
        public bool lucesF { get; set; }
        public bool pi { get; set; }
        public bool pd { get; set; }
        public bool si { get; set; }
        public bool sd { get; set; }
        public string guardaFangosG { get; set; }
        public string guardaFangosI { get; set; }
        public string cintaReflectivaLat { get; set; }
        public string cintaReflectivaFront { get; set; }
        public string cintaReflectivaTra { get; set; }
        public string manitas1 { get; set; }
        public string manitas2 { get; set; }
        public string bumper { get; set; }
        public string fricciones { get; set; }
        public string friccionesLlantas { get; set; }
        public string patas { get; set; }
        public string ganchos { get; set; }
        public string balancines { get; set; }
        public string hojasResortes { get; set; }
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
        public string llanta12 { get; set; }
        public string llantaR { get; set; }
        public string llantaR2 { get; set; }

        public virtual condicionActivosDto condicionActivo { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantas { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantasRepuesto { get; set; }
    }
}
