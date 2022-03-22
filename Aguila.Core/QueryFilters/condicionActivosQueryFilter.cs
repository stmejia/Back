using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Aguila.Core.QueryFilters
{
    public class condicionActivosQueryFilter
    {
        public string tipoCondicion { get; set; }
        public int? idActivo { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public byte? idEmpresa { get; set; }
        public int? idEmpleado { get; set; }
        public int? idReparacion { get; set; }
        public Guid? idImagenRecursoFirma { get; set; }
        public Guid? idImagenRecursoFotos { get; set; }
        public int? ubicacionIdEntrega { get; set; }
        public long? idUsuario { get; set; }
        public string movimiento { get; set; }
        public bool? disponible { get; set; }
        public bool? cargado { get; set; }
        public string serie { get; set; }
        public long? numero { get; set; }
        public bool? inspecVeriOrden { get; set; }
        public int? anio { get; set; }
        public string codigo { get; set; }
        public bool ignorarFechas { get; set; } = false;
        public int? idEstado { get; set; }

        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
