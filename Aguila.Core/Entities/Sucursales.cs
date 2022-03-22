using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Aguila.Core.Entities
{
    public partial class Sucursales
    {
        public Sucursales()
        {
            EstacionesTrabajo = new HashSet<EstacionesTrabajo>();
        }

        public short Id { get; set; }
        public byte EmpresaId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Activa { get; set; }
        public DateTime FchCreacion { get; set; }

        public Empresas Empresa { get; set; }
        [JsonIgnore]
        public ICollection<EstacionesTrabajo> EstacionesTrabajo { get; set; }
    }
}
