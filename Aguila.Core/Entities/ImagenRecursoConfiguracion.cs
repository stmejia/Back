using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class ImagenRecursoConfiguracion
    {
        public long Id { get; set; }

        public int Recurso_Id { get; set; }

        public string Propiedad { get; set; }

        public DateTime FchCreacion { get; set; }

        public string Servidor { get; set; }

        public string Carpeta { get; set; }

        public decimal PesoMaxMb { get; set; }

        public bool EliminacionFisica { get; set; }

        public string DefaultImagen { get; set; }
        public bool MultiplesImagenes { get; set; }
        public byte NoMaxImagenes { get; set; }
      
        [JsonIgnore]
        public  ICollection<ImagenRecurso> ImagenesRecursos { get; set; }
        public virtual string UrlImagenDefaul { get; set; }

        public virtual Recursos Recurso { get; set; }


    }
}
