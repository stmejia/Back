using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface IdireccionesService
    {
        PagedList<direcciones> GetDirecciones(direccionesQueryFilter filter);
        Task<direcciones> GetDireccion(long id);
        Task InsertDireccion(direcciones direccion);
        Task<bool> UpdateDireccion(direcciones direccion);
        Task<bool> DeleteDireccion(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
