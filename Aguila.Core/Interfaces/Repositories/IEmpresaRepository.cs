using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguila.Core.Entities;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IEmpresaRepository: IRepository<Empresas>
    {
        Task<Empresas> GetByIdWithImagenLogo(long Id);
        IQueryable<Empresas> GetAllIncludes();
    }
}
