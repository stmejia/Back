using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aguila.Core.Entities
{
    public class Imagen
    {
        public Guid  Id { get; set; }
        public Guid  ImagenRecurso_Id { get; set; }
        public string FileName { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FchCreacion { get; set; }
        public DateTime? FchBorrada { get; set; }
        public bool ArchivoEliminado { get; set; }
        public string SubirImagenBase64 { get; set; }
        public string UrlImagen { get; set; }

        [JsonIgnore]
        public ImagenRecurso ImagenRecurso { get; set; }
    }
}
