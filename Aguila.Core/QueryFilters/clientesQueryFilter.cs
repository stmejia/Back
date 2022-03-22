using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class clientesQueryFilter
    {
        public int? idTipoCliente { get; set; }
        public long? idDireccion { get; set; }
        public long? idEntidadComercial { get; set; }
        public int? idCorporacion { get; set; }
        public int? diasCredito { get; set; }
        public byte? idEmpresa { get; set; }
        public DateTime? fechaBaja { get; set; }
        public string nit { get; set; } 
        public string codigo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
