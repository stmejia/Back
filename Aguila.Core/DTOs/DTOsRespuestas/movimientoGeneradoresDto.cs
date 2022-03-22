using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    public class movimientoGeneradoresDto
    {
        public int id { get; set; }
        public int idActivo { get; set; }
        public int? ubicacionId { get; set; }
        public int? idRuta { get; set; }
        public int? idEstado { get; set; }
        public int? idServicio { get; set; }
        public int idEstacionTrabajo { get; set; }
        public int? idPiloto { get; set; }
        public long idUsuario { get; set; }
        public long? documento { get; set; }
        public string tipoDocumento { get; set; }
        public string lugar { get; set; }
        public bool? cargado { get; set; }

        public string observaciones { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaCreacion { get; set; }

        public ActivoGeneradoresDto activoOperacion { get; set; }
        public estadosDto estado { get; set; }
        public serviciosDto servicio { get; set; }
        public EstacionesTrabajoDto estacionTrabajo { get; set; }
        public empleadosDto piloto { get; set; }
        public listasDto ubicacion { get; set; }
        public UsuariosDto2 usuario { get; set; }
        public rutasDto ruta { get; set; }
    }
}
