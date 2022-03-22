using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class RecursosDto
    {
        //public RecursosDto()
        //{
        //    this.opciones = new HashSet<string>();
        //}

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<string> opciones { get; set; }
        //public string Opciones { get; set; }
        public string Controlador { get; set; }

        public DateTime fechaCreacion { get; set; }
    }
}
