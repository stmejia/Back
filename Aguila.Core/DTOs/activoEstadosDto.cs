using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class activoEstadosDto
    {
        public int id { get; set; }
        public int idActivo { get; set; }
        public int idEstado { get; set; }
        public string observacion { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
