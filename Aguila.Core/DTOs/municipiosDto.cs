using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class municipiosDto
    {
        //public municipiosDto()
        //{
        //    this.ubicaciones = new HashSet<ubicacionesDto>();
        //}

        public int id { get; set; }
        public int idDepartamento { get; set; }
        public string codMunicipio { get; set; }
        public string nombreMunicipio { get; set; }
        public DateTime? fechaCreacion { get; set; }

        public virtual departamentosDto departamento { get; set; }

        //public virtual ICollection<ubicacionesDto> ubicaciones { get; set; }
    }
}
