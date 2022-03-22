using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class detalleCondicion
    {
        public long id { get; set; }
        public long idUsuario { get; set; }
        public long idUsuarioAutoriza { get; set; }
        public int idCondicion { get; set; }
        public int idReparacion { get; set; }
        public int? cantidad { get; set; }
        public bool? aprobado { get; set; }
        public string nombreAutoriza { get; set; }
        public string observaciones { get; set; }
        public DateTime? fechaAprobacion { get; set; }
        public DateTime fechaEstimadoReparacion { get; set; }
        public DateTime? fechaFinalizacionRep { get; set; }
        public DateTime fechaCreacion { get; set; }

        public Usuarios usuarios { get; set; }
        public Usuarios usuarioAutoriza { get; set; }
        public condicionTallerVehiculo condicionTallerVehiculo { get; set; }
        public reparaciones reparaciones { get; set; }
    }
}
