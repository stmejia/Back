using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    public class condicionActivosDto2
    {

        public long id { get; set; }

        public string tipoCondicion { get; set; }

        public int idActivo { get; set; }

        public long idUsuario { get; set; }

        public long numero { get; set; }

        public DateTime fecha { get; set; }

        public UsuariosDto2 Usuario { get; set; }

    }
}
