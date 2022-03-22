using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IModulosService
    {
        Task<bool> DeleteModulo(byte id);
        Task<Modulos> GetModulo(byte id);
        PagedList<Modulos> GetModulos(ModuloQueryFilter filter);
        Task InsertModulo(Modulos modulo);
        Task<bool> updateModulo(Modulos modulo);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}