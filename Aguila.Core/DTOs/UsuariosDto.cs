using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class UsuariosDto
    {
        public UsuariosDto()
        {
            this.Modulos = new HashSet<ModulosDto>();
            this.Roles = new HashSet<RolesDto>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; }
        public DateTime FchCreacion { get; set; }
        public string Password { get; set; }
        public DateTime FchPassword { get; set; }
        public DateTime fchNacimiento { get; set; }
        public DateTime? fchBloqueado { get; set; }
        public bool cambiarClave { get; set; }
        public byte? ModuloId { get; set; }
        public int? EstacionTrabajoId { get; set; }
        public short SucursalId { get; set; }
        public virtual ICollection<RolesDto> Roles { get; set; }
        public virtual ICollection<ModulosDto> Modulos { get; set; }
        public ImagenRecurso ImagenPerfil { get; set; }
        public ICollection<AsigUsuariosEstacionesTrabajo> EstacionesTrabajoAsignadas { get; set; }

    }
}
