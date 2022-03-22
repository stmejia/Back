using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class EquiposEstatus
    {
        public string TipoEq { get; set; }

        [Key]
        public int EquiID { get; set; }

        public long? EqEvIDUbicacion { get; set; }

        public long? EqEvIDCondicion { get; set; }

        public long? EqEvIDMovimiento { get; set; }

        public long? EqEvIDStatus { get; set; }

        public bool? Disponible { get; set; }

        public int? SlMIID { get; set; }

        public string EstadoLLantas { get; set; }

        public string PredioDestino { get; set; }

        [ForeignKey("EquiID")]
        public EquipoRemolque equipoRemolque { get; set; }

        [ForeignKey("SlMIID")]
        public SolicitudesMovimientosIntegracion solicitudesMovimientosIntegracion { get; set; }

    }
}
