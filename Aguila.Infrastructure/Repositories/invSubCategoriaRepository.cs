using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class invSubCategoriaRepository : _BaseRepository<invSubCategoria>, IinvSubCategoriaRepository
    {
        public invSubCategoriaRepository(AguilaDBContext context) : base(context) { }
    }
}
