using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class asesoresRepository : _BaseRepository<asesores>, IasesoresRepository
    {
        public asesoresRepository(AguilaDBContext context) : base(context) { }
    }
}
