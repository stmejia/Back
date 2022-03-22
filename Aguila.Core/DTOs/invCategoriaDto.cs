using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class invCategoriaDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public byte idEmpresa { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
