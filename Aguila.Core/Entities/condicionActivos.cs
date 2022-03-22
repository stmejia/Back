using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class condicionActivos
    {
        public long id { get; set; }

        public string tipoCondicion { get; set; }

        public int idActivo { get; set; }

        public int idEstacionTrabajo { get; set; }

        public int idEmpleado { get; set; }

        public int? idReparacion { get; set; }

        public int idEstado { get; set; }

        public Guid? idImagenRecursoFirma { get; set; }

        public Guid? idImagenRecursoFotos { get; set; }

        public int? ubicacionIdEntrega { get; set; }

        public long idUsuario { get; set; }

        public string movimiento { get; set; }

        public bool disponible { get; set; }

        public bool cargado { get; set; }

        public string serie { get; set; }

        public long numero { get; set; }

        public bool inspecVeriOrden { get; set; }

        public string observaciones { get; set; }

        public string irregularidadesObserv { get; set; }

        public string daniosObserv { get; set; }

        public DateTime fecha { get; set; }

        public DateTime fechaCreacion { get; set; }

        public activoOperaciones activoOperacion { get; set; }
        public EstacionesTrabajo estacionTrabajo { get; set; }
        public empleados empleado { get; set; }
        public reparaciones reparacion { get; set; }
        public ImagenRecurso ImagenFirmaPiloto { get; set; }
        public ImagenRecurso Fotos { get; set; }
        public Usuarios usuario { get; set; }
        public listas ubicacionEntrega { get; set; }
        public estados estado { get; set; }


    }
}
