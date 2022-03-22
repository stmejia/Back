using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IpilotosDocumentosService
    {
        PagedList<pilotosDocumentos> GetPilotosDocumentos(pilotosDocumentosQueryFilter filter);
        Task<pilotosDocumentos> GetPilotoDocumento(int id);
        Task InsertPilotoDocumento(pilotosDocumentos pilotoDocumento);
        Task<bool> UpdatePilotoDocumento(pilotosDocumentos pilotoDocumento);
        Task<bool> DeletePilotoDocumento(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
