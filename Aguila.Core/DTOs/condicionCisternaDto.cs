using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionCisternaDto
    {
        public long idCondicionActivo { get; set; }
        public string luzLateral { get; set; }
        public string luzTrasera { get; set; }
        public string guardaFango { get; set; }
        public string cintaReflectiva { get; set; }
        public string manitas { get; set; }
        public bool bumper { get; set; }
        public bool patas { get; set; }
        public bool ganchos { get; set; }
        public string fricciones { get; set; }
        public string friccionesLlantas { get; set; }
        public string escalera { get; set; }
        public string señalizacion { get; set; }
        public string taponValvulas { get; set; }
        public string manHole { get; set; }
        public string platinas { get; set; }
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
