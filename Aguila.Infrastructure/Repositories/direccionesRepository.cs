using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class direccionesRepository : _BaseRepository<direcciones>, IdireccionesRepository
    {
        public direccionesRepository(AguilaDBContext context) : base(context) { }
    }
}
