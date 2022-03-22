using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class ubicaciones
    {
        public int id { get; set; }
        public int idMunicipio { get; set; }
        public byte? idEmpresa { get; set; }
        public string codigo { get; set; }
        public bool esPuerto { get; set; }
        public string lugar { get; set; }
        public string codigoPostal { get; set; }
        public decimal? latitud { get; set; }
        public decimal? longitud { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual municipios municipio { get; set; }
        public virtual Empresas empresa { get; set; }
    }
}
