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
    public class RecursosRepository : _BaseRepository<Recursos>, IRecursosRepository
    {
        
        public RecursosRepository(AguilaDBContext context) : base(context) { }


        public async Task<Recursos> GetByControladorNombre(string controladorNombre)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Controlador.Equals(controladorNombre));
        }


    }
}
