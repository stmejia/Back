using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class paises
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodMoneda { get; set; }
        public string CodAlfa2 { get; set; }
        public string CodAlfa3 { get; set; }
        public int CodNumerico { get; set; }
        public string Idioma { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
