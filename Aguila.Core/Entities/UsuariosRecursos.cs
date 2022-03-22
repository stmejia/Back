using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Aguila.Core.Entities
{
    public partial class UsuariosRecursos
    {
        public long id { get; set; }
        public int estacionTrabajo_id { get; set; }
        public int recurso_id { get; set; }
        public long usuario_id { get; set; }
        public string opcionesAsignadas { get; set; }

        public virtual EstacionesTrabajo Estacion { get; set; }
        public virtual Recursos Recurso { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
