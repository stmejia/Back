﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class tipoVehiculosDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public byte idEmpresa { get; set; }
         
        public string descripcion { get; set; }
        public string prefijo { get; set; }
        //public byte? correlativoLongitud { get; set; }
        public string estructuraCoc { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
