using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Aguila.Infrastructure.Repositories
{
    // Podriamos utilizar en lugar de class una baseentity
    public class _BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly AguilaDBContext _context;
        protected readonly DbSet<T> _entities;

        public _BaseRepository(AguilaDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public DbContext DbContext { get { return _context; } }

        //public IEnumerable<T> GetAll()
        //{
        //    // esto es una consulta diferida , es decir aun no se ah ejecutado solo esta preparada
        //    // se ejecuta cuando decimos tolist, o recorremos con un foreach
        //    return _entities.AsEnumerable();
        //}

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public async Task<T> GetByID(object id)
        {         
            return await _entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            // utilizamos UnitOfWork para enviar los cambios a la base de datos, ya no lo hacemos desde cada repositorio
            // La vengaja de utilizarlo en el UnitOfWork, el poder trabajar con transacciones y guardar en varias tablas en una sola transaccion
            // await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(object id)
        {
            T entity = await GetByID(id);
            _entities.Remove(entity);
        }
    }
}
