using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Core
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly KaizenDbContext context;
        protected readonly DbSet<T> entities;

        public BaseRepository(KaizenDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);
        }

        public bool Delete(T entity)
        {
            context.Remove(entity);
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}