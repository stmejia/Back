using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IestadosRepository : IRepository<estados>
    {
        estados GetEstadoByEvento(int empresaId, string tipo, string evento);
    }
}
