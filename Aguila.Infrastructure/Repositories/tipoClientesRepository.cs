using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class tipoClientesRepository : _BaseRepository<tipoClientes>, ItipoClientesRepository
    {
        public tipoClientesRepository(AguilaDBContext context) : base(context) { }
    }
}
