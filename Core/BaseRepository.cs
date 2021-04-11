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
        protected readonly DbSet<T> entites;

        public BaseRepository(KaizenDbContext context)
        {
            this.context = context;
            this.entites = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await entites.AddAsync(entity);
        }

        public bool Delete(T entity)
        {
            context.Remove(entity);
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return entites.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entites.FindAsync(id);
        }

        public void Update(T entity)
        {
            entites.Update(entity);
        }
    }
}