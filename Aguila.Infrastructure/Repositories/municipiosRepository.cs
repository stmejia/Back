using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class municipiosRepository : _BaseRepository<municipios>, ImunicipiosRepository
    {
        public municipiosRepository(AguilaDBContext context) : base(context) { }
    }
}
