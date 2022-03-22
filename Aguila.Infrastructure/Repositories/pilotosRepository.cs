using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class pilotosRepository : _BaseRepository<pilotos>, IpilotosRepository
    {
        public pilotosRepository(AguilaDBContext context) : base(context) { }
    }
}
