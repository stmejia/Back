using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class ubicacionesDto
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


        public virtual string vDireccion { get; set; }
        public virtual int idDepartamento { get; set; }
        public virtual int idPais { get; set; }
    }
}
