using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Aguila.Core.Interfaces.Repositories;

namespace Aguila.Infrastructure.Repositories
{
    public class AsigUsuariosRecursosAtributosRepository : _BaseRepository<AsigUsuariosRecursosAtributos>, IAsigUsuariosRecursosAtributosRepository
    {
        
        public AsigUsuariosRecursosAtributosRepository(AguilaDBContext context): base(context) { }



    }
}
