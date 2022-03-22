using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class ImagenRecurso
    {
        public Guid Id { get; set; }
        public long ImagenRecursoConfig_Id { get; set; }
        public Guid?  Imagen_IdDefault { get; set; }
        public Imagen ImagenDefault { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        // relaciones
        [JsonIgnore]
        public Usuarios Usuario { get; set; }
        [JsonIgnore]
        public virtual ImagenRecursoConfiguracion ImagenRecursoConfiguracion { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Guid> ImagenesEliminar { get; set; }

    }
}
