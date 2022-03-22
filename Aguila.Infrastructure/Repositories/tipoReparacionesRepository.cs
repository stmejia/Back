using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    class tipoReparacionesRepository: _BaseRepository<tipoReparaciones>, ItipoReparacionesRepository
    {
        public tipoReparacionesRepository(AguilaDBContext context) : base(context) { }
    }
}
