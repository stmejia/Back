using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class equipoRemolqueQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idTipoEquipoRemolque { get; set; }
        public string tarjetaCirculacion { get; set; }
        public string placa { get; set; }        
        public string codigo { get; set; }
        public byte? idEmpresa { get; set; }
        public int? idEstado { get; set; }
        public string flota { get; set; }
        public bool? propio { get; set; }
        public bool? equipoActivo { get; set; }

        //COC
        public int? noEjes { get; set; }
        public string tandemCorredizo { get; set; }
        public string chasisExtensible { get; set; }
        public string tipoCuello { get; set; }
        public string acopleGenset { get; set; }
        public string acopleDolly { get; set; }
        public string capacidadCargaLB { get; set; }
        public string medidaLB { get; set; }
        public string medidaPlataforma { get; set; }  
        public string pechera { get; set; }
        public string alturaContenedor { get; set; }
        public string tipoContenedor { get; set; }
        public string marcaUR { get; set; }
        public string largoFurgon { get; set; }
        //public string rielesHorizontales { get; set; }
        //public string rielesVerticales { get; set; }
        public string suspension { get; set; }
        public string rieles { get; set; }
        //FIN COC

        public bool ignorarFechas { get; set; } = false;
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }

        public bool? global { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
