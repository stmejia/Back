using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class responseCondicionCabezalDto
    {
        public long id { get; set; }

        public string tipoCondicion { get; set; }

        public int idActivo { get; set; }

        public int idEstacionTrabajo { get; set; }

        public int idEmpleado { get; set; }

        public int? idReparacion { get; set; }

        public Guid? idImagenRecursoFirma { get; set; }

        public Guid? idImagenRecursoFotos { get; set; }

        public string ubicacionIdEntrega { get; set; }

        public long idUsuario { get; set; }

        public string movimiento { get; set; }

        public bool disponible { get; set; }

        public bool cargado { get; set; }

        public string serie { get; set; }

        public long numero { get; set; }

        public bool inspecVeriOrden { get; set; }

        public string observaciones { get; set; }

        public DateTime fecha { get; set; }

        public DateTime fechaCreacion { get; set; }

        public virtual condicionCabezalDto condicionDetalle { get; set; }
    }
}
