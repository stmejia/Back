using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    //No tiene activo de operacion , evitamos la carga Ciclica
    public class generadoresDto2
    {
        public int idActivo { get; set; }
        public int idTipoGenerador { get; set; }
        public decimal capacidadGalones { get; set; }
        public int numeroCilindros { get; set; }
        public string marcaGenerador { get; set; }
        public string tipoInstalacion { get; set; }
        public string tipoEnfriamiento { get; set; }
        public int idEstacion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public tipoGeneradoresDto tipoGenerador { get; set; }
    }
}
