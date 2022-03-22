using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class entidadesComercialesDireccionesRepository : _BaseRepository<entidadesComercialesDirecciones>, IentidadesComercialesDireccionesRepository
    {
        public entidadesComercialesDireccionesRepository(AguilaDBContext context) : base(context) { }
    }
}
