using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionActivosDto
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

        public virtual object condicionDetalle { get; set; }

        public activoOperacionesDto activoOperacion { get; set; }
        public EstacionesTrabajoDto estacionTrabajo { get; set; }
        public empleadosDto empleado { get; set; }
        public reparacionesDto reparacion { get; set; }
        public ImagenRecurso ImagenFirmaPiloto { get; set; }
        public ImagenRecurso Fotos { get; set; }

        public UsuariosDto2 Usuario { get; set; }        
        public listasDto ubicacionEntrega { get; set; }
        public estadosDto estado { get; set; }

        //public virtual string vUsuario { get; set; }
    }
}
