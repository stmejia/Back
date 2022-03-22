using Aguila.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Aguila.Core.DTOs
{
    public class SucursalDto
    {
        //public SucursalDto()
        //{
        //    this.Empresas = new HashSet<EmpresasDto>();
        //}

        public short Id { get; set; }
        public byte EmpresaId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Activa { get; set; }
        public DateTime FchCreacion { get; set; }

        //public virtual ICollection<EmpresasDto> Empresas { get; set; }
        public EmpresasDto Empresa { get; set; }
        [JsonIgnore]
        public ICollection<EstacionesTrabajo> EstacionesTrabajo { get; set; }
    }
}
