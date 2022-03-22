using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Aguila.Infrastructure.Repositories
{
    public class estadosRepository : _BaseRepository<estados>, IestadosRepository
    {
        public estadosRepository(AguilaDBContext context) : base(context) { }

        public estados GetEstadoByEvento(int empresaId, string tipo, string evento)
        {
            return _entities.Where(e => e.idEmpresa == empresaId && e.tipo.ToUpper().Trim() == tipo.ToUpper().Trim() && e.evento.ToUpper().Contains(evento.ToUpper().Trim())).FirstOrDefault();
        }

    }
}
