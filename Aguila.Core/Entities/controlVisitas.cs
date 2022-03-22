using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class controlVisitas
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string identificacion { get; set; }
        public string motivoVisita { get; set; }
        public string areaVisita { get; set; }
        public string nombreQuienVisita { get; set; }
        public string? vehiculo { get; set; }
        public DateTime ingreso { get; set; }
        public DateTime? salida { get; set; } 
        public long idUsuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int idEstacionTrabajo { get; set; }
        public string empresaVisita { get; set; }
        public Guid? idImagenRecursoDpi { get; set; }

        public virtual Usuarios usuario { get; set; }
        public virtual EstacionesTrabajo estacion { get; set; }

        public ImagenRecurso DPI { get; set; }
    }
}
