using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class clienteTarifasRepository : _BaseRepository<clienteTarifas>, IclienteTarifasRepository
    {
        public clienteTarifasRepository(AguilaDBContext context) : base(context) { }
    }
}
