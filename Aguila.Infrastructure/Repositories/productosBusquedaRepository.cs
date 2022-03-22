using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class productosBusquedaRepository : _BaseRepository<productosBusqueda>, IproductosBusquedaRepository
    {
        public productosBusquedaRepository(AguilaDBContext context) : base(context) { }
    }
}
