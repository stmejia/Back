﻿using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class EstacionesTrabajoDto
    {
        public int Id { get; set; }
        public short SucursalId { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activa { get; set; }       
        public DateTime FchCreacion { get; set; }
        public SucursalDto Sucursal { get; set; }

    }
}
