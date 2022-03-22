using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Aguila.Infrastructure.Repositories
{
    public class RecursosAtributosRepository : _BaseRepository<RecursosAtributos>, IRecursosAtributosRepository
    {
        
        public RecursosAtributosRepository(AguilaDBContext context) : base(context) { }



    }
}
