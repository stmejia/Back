using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IRecursosAtributosService
    {
        Task<bool> DeleteRecursoAtributo(int id);
        Task<RecursosAtributos> GetRecursoAtributo(int id);
        PagedList<RecursosAtributos> GetRecursosAtributos(RecursosAtributosQueryFilter filter);
        Task InsertRecursoAtributo(RecursosAtributos recursoAtributo);
        Task<bool> updateRecursoAtributo(RecursosAtributos recursoAtributo);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}