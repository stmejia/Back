using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.QueryFilters
{
    public class ReporteMovimientosEquiposRemolqueQueryFilter
    {
        //Global para Movimientos
        public byte idEmpresa { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public string? codigo { get; set; }
        public int? idActivo { get; set; }
                
        public string? listaIdEstados { get; set; }
        public string? flota { get; set; }
        public bool? propio { get; set; }
        public bool? equipoActivo { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public int idUsuario { get; set; }
        public string tipoDocumento { get; set; }
        public int? documento { get; set; }

        public int? idTipoEquipoRemolque { get; set; }
        public string tarjetaCirculacion { get; set; }
        public string placa { get; set; }

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
    }
}
