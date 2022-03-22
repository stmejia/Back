using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    public class ActivoGeneradoresDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime? fechaBaja { get; set; }
        public string categoria { get; set; }
        public string color { get; set; }
        public string marca { get; set; }
        public string vin { get; set; }
        public int correlativo { get; set; }
        public string serie { get; set; }
        public int? modeloAnio { get; set; }
        public int? idActivoGenerales { get; set; }
        public int idTransporte { get; set; }
        public string flota { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string coc { get; set; }
        public byte idEmpresa { get; set; }

        public transportesDto transporte { get; set; }
        public generadoresDto2 generadores { get; set; }
    }
}
