using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class ModulosMnu
    {
        public ModulosMnu()
        {
            InverseMenuIdPadreNavigation = new HashSet<ModulosMnu>();
        }

        public int Id { get; set; }
        public byte ModuloId { get; set; }
        public int MenuIdPadre { get; set; }
        public short Codigo { get; set; }
        public string Descrip { get; set; }
        public int? RecursoId { get; set; }
        public bool Activo { get; set; }

        public virtual ModulosMnu MenuIdPadreNavigation { get; set; }
        public virtual Modulos Modulo { get; set; }
        public virtual Recursos Recurso { get; set; }
        public virtual ICollection<ModulosMnu> InverseMenuIdPadreNavigation { get; set; }
    }
}
