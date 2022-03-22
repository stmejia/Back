using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class paisesDto
    {
        //public paisesDto()
        //{
        //    this.departamentos = new HashSet<departamentosDto>();
        //}

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodMoneda { get; set; }
        public string CodAlfa2 { get; set; }
        public string CodAlfa3 { get; set; }
        public int CodNumerico { get; set; }
        public string Idioma { get; set; }
        public DateTime FechaCreacion { get; set; }

        //public virtual ICollection<departamentosDto> departamentos { get; set; }
    }
}
