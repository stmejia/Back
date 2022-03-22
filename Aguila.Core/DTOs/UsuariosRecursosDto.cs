using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class UsuariosRecursosDto
    {
        //public UsuariosRecursosDto()
        //{
        //    this.opcionesAsignadas = new HashSet<string>();
        //}

        public long id { get; set; }
        public int estacionTrabajo_id { get; set; }
        public int recurso_id { get; set; }
        public long usuario_id { get; set; }        
        public virtual ICollection<string> opcionesAsignadas { get; set; }

        public virtual EstacionesTrabajoDto Estacion { get; set; }
        public virtual RecursosDto Recurso { get; set; }
    }
}
