using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class tipoGeneradoresRepository : _BaseRepository<tipoGeneradores>, ItipoGeneradoresRepository
    {
        public tipoGeneradoresRepository(AguilaDBContext context) : base(context) { }
    }
}
