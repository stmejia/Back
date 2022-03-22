using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class activoMovimientosActualDto
    {
        
        public int idActivo { get; set; }
        public int idEstado { get; set; }
        public int idEstacionTrabajo { get; set; }
        public int? idServicio { get; set; }
        public int? idEmpleado { get; set; }
        public int? ubicacionId { get; set; }
        public int? idRuta { get; set; }
        public string lugar { get; set; }
        public long idUsuario { get; set; }
        public int documento { get; set; }
        public string tipoDocumento { get; set; }
        public bool? cargado { get; set; }
        public string observaciones { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaCreacion { get; set; }
        public virtual int vDiasUltMov { get { return (DateTime.Now - fecha).Days; } set { } }

        public activoOperacionesDto activoOperacion { get; set; }
        public estadosDto estado { get; set; }
        public EstacionesTrabajoDto estacionTrabajo { get; set; }
        public serviciosDto servicio { get; set; }
        public empleadosDto empleado { get; set; }
        public rutasDto ruta { get; set; }
        public UsuariosDto2 usuario { get; set; }


    }
}
