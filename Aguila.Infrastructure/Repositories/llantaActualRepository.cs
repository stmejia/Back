using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class llantaActualRepository : _BaseRepository<llantaActual>, IllantaActualRepository
    {
        public llantaActualRepository(AguilaDBContext context) : base(context) { }
    }
}
