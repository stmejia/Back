using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IModulosMnuService
    {
        Task<bool> DeleteModuloMnu(int id);
        Task<ModulosMnu> GetModuloMnu(int id);
        PagedList<ModulosMnu> GetModulosMnu(ModulosMnuQueryFilter filter);
        Task InsertModuloMnu(ModulosMnu modulosMnu);
        Task<bool> updateModuloMnu(ModulosMnu modulosMnu);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}