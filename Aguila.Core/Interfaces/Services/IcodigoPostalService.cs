using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcodigoPostalService
    {
        Task<codigoPostal> getCodigo(int id);
        List<codigoPostal> getCodigosDepartamento(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}