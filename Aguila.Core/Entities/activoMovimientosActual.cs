using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class activoMovimientosActual
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
        public long? documento { get; set; }
        public string tipoDocumento { get; set; }
        public bool? cargado { get; set; }

        public string observaciones { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaCreacion { get; set; }

        public  activoOperaciones activoOperacion { get; set; }
        public  estados estado { get; set; }
        public  EstacionesTrabajo estacionTrabajo { get; set; }
        public  servicios servicio { get; set; }
        public  empleados empleado { get; set; }
        public  rutas ruta { get; set; }
        public Usuarios usuario { get; set; }
    }
}
