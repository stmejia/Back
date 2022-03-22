using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IclienteTarifasService
    {
        PagedList<clienteTarifas> GetClienteTarifas(clienteTarifasQueryFilter filter);
        Task<clienteTarifas> GetClienteTarifa(int id);
        Task InsertClienteTarifa(clienteTarifas clienteTarifa);
        Task<bool> UpdateClienteTarifa(clienteTarifas clienteTarifa);
        Task<bool> DeleteClienteTarifa(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
