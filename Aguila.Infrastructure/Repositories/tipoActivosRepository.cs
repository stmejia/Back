using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using Aguila.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class tipoActivosRepository : _BaseRepository<tipoActivos>, ItipoActivosRepository
    {
        public tipoActivosRepository(AguilaDBContext context) : base(context) { }
    }
}
