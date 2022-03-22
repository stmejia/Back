using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class llantaActual
    {
        public int idLlanta { get; set; }
        public int idLlantaTipo { get; set; }
        public int idActivoOperaciones { get; set; }
        public int idEstado { get; set; }
        public int ubicacionId { get; set; }
        public string documentoEstado { get; set; }
        public string documentoUbicacion { get; set; }
        public string observacion { get; set; }
        public int posicion { get; set; }
        public string profundidadIzquierda { get; set; }
        public string profundidadCentro { get; set; }
        public string profundidadDerecho { get; set; }
        public string reencauche { get; set; }
        public decimal precio { get; set; }
        public string proposito { get; set; }
        public DateTime fechaEstado { get; set; }
        public DateTime fechaUbicacion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual llantas llanta { get; set; }
        public virtual llantaTipos llantaTipo { get; set; }
        public virtual activoOperaciones activoOperacion { get; set; }
        public virtual estados estado { get; set; }
        public virtual ubicaciones ubicacion { get; set; }
    }
}
