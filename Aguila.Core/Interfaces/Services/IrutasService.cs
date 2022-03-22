using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IrutasService
    {
        PagedList<rutas> GetRutas(rutasQueryFilter filter);
        Task<rutas> GetRuta(int id);
        Task InsertRuta(rutas ruta);
        Task<bool> UpdateRuta(rutas ruta);
        Task<bool> DeleteRuta(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
