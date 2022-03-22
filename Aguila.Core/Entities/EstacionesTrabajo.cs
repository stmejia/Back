using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class EstacionesTrabajo
    {
        public int Id { get; set; }
        public short SucursalId { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activa { get; set; }
        public DateTime FchCreacion { get; set; }
        public Sucursales Sucursal { get; set; }
        public ICollection<AsigUsuariosEstacionesTrabajo> AsigUsuariosEstacionesTrabajo { get; set; }
    }
}
