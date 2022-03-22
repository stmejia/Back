using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class departamentosRepository : _BaseRepository<departamentos>, IdepartamentosRepository
    {
        public departamentosRepository(AguilaDBContext context) : base(context) { }
    }
}
