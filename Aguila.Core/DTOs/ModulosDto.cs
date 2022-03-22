using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class ModulosDto
    {
        public byte Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string path { get; set; }
        public string ModuMinVersion { get; set; }
    }
}
