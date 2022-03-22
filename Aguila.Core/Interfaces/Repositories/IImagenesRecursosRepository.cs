using Aguila.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IImagenesRecursosRepository: IRepository<ImagenRecurso>
    {
        Task<ImagenRecurso> GetByIdWithConfiguracion(Guid Id);
    }
}
