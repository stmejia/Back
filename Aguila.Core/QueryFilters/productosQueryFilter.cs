using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class productosQueryFilter
    {
        public string codigo { get; set; }
        public string codigoQR { get; set; }
        public string descripcion { get; set; }
        public string bienServicio { get; set; }
        public int? idsubCategoria { get; set; }
        public int? idMedida { get; set; }
        public byte? idEmpresa { get; set; }
        public DateTime? fechaBaja { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
