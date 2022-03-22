using Aguila.Core.DTOs.DTOsRespuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs
{
    public class eventosControlEquipoDto
    {
        public long id { get; set; }
        public int idActivo { get; set; }
        public long idUsuarioCreacion { get; set; }
        public int idEstacionTrabajo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string descripcionEvento { get; set; }
        public string bitacoraObservaciones { get; set; }
        public DateTime? fechaRevisado { get; set; }
        public long? idUsuarioRevisa { get; set; }
        public DateTime? fechaResuelto { get; set; }
        public long? idUsuarioResuelve { get; set; }
        public DateTime? fechaAnulado { get; set; }
        public long? idUsuarioAnula { get; set; }
        public virtual string estado { get { if (fechaAnulado != null) { return "ANULADO"; }
                else if (fechaResuelto != null) { return "RESUELTO"; }
                else if (fechaRevisado !=null) { return "REVISADO"; }
                else { return "CREADO"; }
            } set { } }

        public activoOperacionesDto activoOperacion { get; set; }
        public UsuariosDto2 usuarioCreacion { get; set; }
        public EstacionesTrabajoDto estacionTrabajo { get; set; }
        public UsuariosDto2 usuarioRevisa { get; set; }
        public UsuariosDto2 usuarioResuelve { get; set; }
        public UsuariosDto2 usuarioAnula { get; set; }
    }
}
