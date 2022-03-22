using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class condicionCabezal
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
        public string llantaR2 { get; set; }

        public virtual condicionActivos condicionActivo { get; set; }
        //public virtual activoOperaciones activoOperacion { get; set; }
        //public virtual estados estado { get; set; }
        //public virtual reparaciones reparacion { get; set; }
        //public virtual pilotos piloto { get; set; }
        //public virtual Usuarios usuario { get; set; }
    }
}
