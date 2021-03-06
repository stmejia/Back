using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class empleadosDto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string codigoAnterior { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string dpi { get; set; }
        public string nit { get; set; }
        public long idDireccion { get; set; }
        public long telefono { get; set; }
        public DateTime fechaAlta { get; set; }
        public string licenciaConducir { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DateTime? fechaBaja { get; set; }
        public byte idEmpresa { get; set; }
        public string pais { get; set; }
   
        public string correlativo { get; set; }
        public string departamento { get; set; }
        public string area { get; set; }
        public string subArea { get; set; }
        public string puesto { get; set; }
        public string categoria { get; set; }
        public string localidad { get; set; }
        public byte idEmpresaEmpleador { get; set; }
        public string estado { get; set; }
        public string dependencia { get; set; }
        public string cop { get; set; }
        public string placas { get; set; }
        public Guid? idImagenRecursoFotografias { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual direccionesDto direccion { get; set; }
       
        public ImagenRecurso Fotos { get; set; }
    }
}
