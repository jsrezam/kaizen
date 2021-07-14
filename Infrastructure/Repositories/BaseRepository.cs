using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
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
        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await entities.AddRangeAsync(entity);
        }

        public bool Delete(T entity)
        {
            context.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllNoTracking()
        {
            return await entities.AsNoTracking().ToListAsync();
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