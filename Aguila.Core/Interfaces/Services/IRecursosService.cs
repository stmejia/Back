using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IRecursosService
    {
        Task<bool> DeleteRecurso(int id);
        Task<Recursos> GetRecurso(int id);
        PagedList<Recursos> GetRecursos(RecursosQueryFilter filter);
        Task InsertRecurso(Recursos recurso);
        Task<bool> updateRecurso(Recursos recurso);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        List<Recursos> GetRecursosGeneral(RecursosQueryFilter filter);
    }
}