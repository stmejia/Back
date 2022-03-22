using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        //IEnumerable<T> GetAll();
        Task<T> GetByID(object id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(object id);
    }
}
