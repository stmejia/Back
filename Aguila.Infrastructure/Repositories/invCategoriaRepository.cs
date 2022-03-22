using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class invCategoriaRepository : _BaseRepository<invCategoria>, IinvCategoriaRepository
    {
        public invCategoriaRepository(AguilaDBContext context) : base(context) { }
    }
}
