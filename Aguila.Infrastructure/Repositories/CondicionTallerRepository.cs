using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class CondicionTallerRepository : _BaseRepository<CondicionTaller>, ICondicionTallerRepository
    {
        public CondicionTallerRepository(AguilaDBContext context) : base(context) { }
    }



}
