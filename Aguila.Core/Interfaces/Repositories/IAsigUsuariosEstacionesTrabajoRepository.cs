﻿using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IAsigUsuariosEstacionesTrabajoRepository : IRepository<AsigUsuariosEstacionesTrabajo>
    {
        IQueryable<AsigUsuariosEstacionesTrabajo> GetEstacionesUsuario(long id);
    }
}