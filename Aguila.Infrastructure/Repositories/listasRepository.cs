using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using Aguila.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class listasRepository: _BaseRepository<listas> , IlistasRepository
    {
        public listasRepository(AguilaDBContext context) : base(context) { }
       
    }
}
