using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoOperacionesService
    {
        Task<PagedList<activoOperaciones>> GetActivoOperaciones(activoOperacionesQueryFilter filter);
        Task<activoOperaciones> GetActivoOperacion(int id);
        Task<ingresoDto> GetActivoOperacionCodigo(activoOperaciones activo);
        Task InsertActivoOperacion(activoOperaciones activoOperacion);
        //Task <ingresoDto> InsertActivoOperacionCodigo(ingresoDto ingreso, int usuario);
        Task<bool> UpdateActivoOperacion(activoOperaciones activoOperacion);
        Task<bool> DeleteActivoOperacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
