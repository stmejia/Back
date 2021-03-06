using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    public class activoMovimientosDto2
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
        public virtual byte idEmpresa { get; set; }
        public string guardiaNombre { get; set; }
        public long condicion { get; set; }
        public string tipoEquipo { get; set; }
        public string piloto2 { get; set; }

        public estadosDto estado { get; set; }
        public serviciosDto servicio { get; set; }
        public EstacionesTrabajoDto estacionTrabajo { get; set; }
        public empleadosDto empleado { get; set; }
        public listasDto ubicacion { get; set; }
        public UsuariosDto2 usuario { get; set; }
        public rutasDto ruta { get; set; }
    }
}
