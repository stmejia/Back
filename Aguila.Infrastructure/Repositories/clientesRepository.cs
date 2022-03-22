using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class clientesRepository : _BaseRepository<clientes>, IclientesRepository
    {
        public clientesRepository(AguilaDBContext context) : base(context) { }
    }
}
