using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IUsuariosService
    {
        Task<Usuarios> GetUsuario(long id);
        //Task<IEnumerable<Usuarios>> GetUsuarios();
        Task<PagedList<Usuarios>> GetUsuarios(UsuarioQueryFilter filter);
        Task InsertUsuario(Usuarios usuario);
        Task<bool> UpdateUsuario(Usuarios usuario);
        Task<bool> DeleteUsuario(long id);
        Task<Usuarios> GetUsuarioByUserName(string username);
        Task<bool> Bloquear(long id);
        Task<bool> CambiarClave(UsuarioLogin userioLogin, bool restablecer);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}