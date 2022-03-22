using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class departamentosDto
    {
        //public departamentosDto()
        //{
        //    this.municipios = new HashSet<municipiosDto>();
        //}

        public int id { get; set; }
        public int idPais { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public virtual paisesDto pais { get; set; }

        //public virtual ICollection<municipiosDto> municipios { get; set; }
    }
}
