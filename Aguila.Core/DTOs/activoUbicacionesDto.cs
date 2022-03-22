using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class activoUbicacionesDto
    {
        public int id { get; set; }
        public int idActivo { get; set; }
        public int idUbicacion { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
