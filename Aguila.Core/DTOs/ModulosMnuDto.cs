using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class ModulosMnuDto
    {
        public int Id { get; set; }
        public byte ModuloId { get; set; }
        public int MenuIdPadre { get; set; }
        public short Codigo { get; set; }
        public string Descrip { get; set; }
        public int? RecursoId { get; set; }
        public bool Activo { get; set; }
    }
}
