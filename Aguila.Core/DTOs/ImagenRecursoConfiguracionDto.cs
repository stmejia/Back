using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class ImagenRecursoConfiguracionDto
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
        public string SubirImagenBase64 { get; set; }
       

    }
}
