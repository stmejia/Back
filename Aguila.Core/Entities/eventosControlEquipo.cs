using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class eventosControlEquipo
    {
        public long id { get; set; }
        public int idActivo { get; set; }
        public long idUsuarioCreacion { get; set; }
        public int idEstacionTrabajo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string descripcionEvento { get; set; }
        public string bitacoraObservaciones { get; set; }
        public DateTime? fechaRevisado { get; set; }
        public long? idUsuarioRevisa { get; set; }
        public DateTime? fechaResuelto { get; set; }
        public long? idUsuarioResuelve { get; set; }
        public DateTime? fechaAnulado { get; set; }
        public long? idUsuarioAnula { get; set; }

        public activoOperaciones activoOperacion { get; set; }
        public Usuarios usuarioCreacion { get; set; }
        public EstacionesTrabajo estacionTrabajo { get; set; }
        public Usuarios usuarioRevisa { get; set; }
        public Usuarios usuarioResuelve { get; set; }
        public Usuarios usuarioAnula { get; set; }

    }
}
