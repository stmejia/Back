using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class ImagenRecursoConfiguracionQueryFilter
    {
        public int? Recurso_Id { get; set; }
        public string Propiedad { get; set; }
        public string DefaultImagen { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
