using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class controlEquipoAjeno
    {
        public long id { get; set; }

        public string nombrePiloto { get; set; }

        public string placaCabezal { get; set; }

        public string codigoEquipo { get; set; }

        public string tipoEquipo { get; set; }

        public string codigoChasis { get; set; }

        public string codigoGenerador { get; set; }

        public DateTime? ingreso { get; set; }

        public DateTime? salida { get; set; }

        public bool? cargado { get; set; }

        public string origen { get; set; }

        public string destino { get; set; }

        public string marchamo { get; set; }

        public bool? atc { get; set; }

        public long idUsuario { get; set; }

        public int idEstacionTrabajo { get; set; }
        public string empresa { get; set; }

        public DateTime fechaCreacion { get; set; }

        public virtual Usuarios usuario { get; set; }
        public virtual EstacionesTrabajo estacion { get; set; }
    }
}
