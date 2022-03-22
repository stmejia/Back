using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class invProductoBodegaRepository : _BaseRepository<invProductoBodega>, IinvProductoBodegaRepository
    {
        public invProductoBodegaRepository(AguilaDBContext context) : base(context) { }
    }
}
