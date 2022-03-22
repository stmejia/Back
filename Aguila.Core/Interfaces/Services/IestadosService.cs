using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IestadosService
    {
        PagedList<estados> GetEstados(estadosQueryFilter filter);
        Task<estados> GetEstado(int id);
        Task InsertEstado(estados estado);
        Task<bool> UpdateEstado(estados estado);
        Task<bool> DeleteEstado(int id);
        bool existeDato(string codigo, byte empresa, int numeroOrden);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        estados GetEstadoByEvento(int empresaId, string tipo, string evento);
        IEnumerable<estados> GetEstadosByEvento(int idEmpresa, string tipo, List<string> eventos);
        //List<estados> GetEstadosByEvento(int idEmpresa, string tipo, List<string> eventos);
    }
}
