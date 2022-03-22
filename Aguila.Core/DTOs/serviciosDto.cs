using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class serviciosDto
    {
        public int id { get; set; }
        public byte idEmpresa { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public bool ruta { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
