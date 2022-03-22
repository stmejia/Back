using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class listasDto
    {
        public int id { get; set; }

        public string valor { get; set; }

        public string descripcion { get; set; }

        public byte idEmpresa { get; set; }

        public int idTipoLista { get; set; }

        public DateTime fechaCreacion { get; set; }
    }
}
