using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class Recursos
    {
        public Recursos()
        {
            ModulosMnu = new HashSet<ModulosMnu>();
            RecursosAtributos = new HashSet<RecursosAtributos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }
        //public string opcionesAsignadas { get; set; }
        public string opciones { get; set; }
        public string Controlador { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual ICollection<ModulosMnu> ModulosMnu { get; set; }
        public virtual ICollection<RecursosAtributos> RecursosAtributos { get; set; }
    }
}
