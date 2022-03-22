using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionCabezalDto
    {
        public long idCondicionActivo { get; set; }              
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

        public virtual condicionActivosDto condicionActivo { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantas { get; set; }
        public virtual ICollection<condicionLlantaDto> condicionesLlantasRepuesto { get; set; }
    }
}
