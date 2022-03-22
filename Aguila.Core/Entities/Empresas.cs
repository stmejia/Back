using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Aguila.Core.Entities
{
    public partial class Empresas
    {
        public Empresas()
        {
            Sucursales = new HashSet<Sucursales>();
        }
                
        public byte Id { get; set; }
        public byte Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public string Aleas { get; set; }
        public bool Activ { get; set; }
        public DateTime FchCreacion { get; set; }
        public bool esEmpleador { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public Guid? ImagenRecurso_IdLogo { get; set; }
        public ImagenRecurso ImagenLogo { get; set; }
        [JsonIgnore]
        public ICollection<Sucursales> Sucursales { get; set; }
    }
}
