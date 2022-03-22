using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class empleadosIngresos
    {
        public long id { get; set; }
        public int idEmpleado { get; set; }
        public string cui { get; set; }
        public string evento { get; set; }
        public DateTime fechaEvento { get; set; }
        public string? vehiculo { get; set; }
        public int idEstacionTrabajo { get; set; }
        public long idUsuario { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual empleados empleado { get; set; }
        public virtual Usuarios usuario { get; set; }
        public virtual EstacionesTrabajo estacion { get; set; }
    }
}
