using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<PagedList<Empresas>> GetEmpresas(EmpresaQueryFilter filter);
        Task<Empresas> GetEmpresa(byte id);
        Task InsertEmpresa(Empresas empresa);
        Task<bool> updateEmpresa(Empresas empresa);
        Task<bool> DeleteEmpresa(byte id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}