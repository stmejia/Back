using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class Modulos
    {
        public Modulos()
        {
            ModulosMnu = new HashSet<ModulosMnu>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string path { get; set; }
        public string ModuMinVersion { get; set; }

        public virtual ICollection<ModulosMnu> ModulosMnu { get; set; }
    }
}
