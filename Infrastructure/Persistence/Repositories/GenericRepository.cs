using Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        public async Task AddAsync(TEntity entity)
        {
            await storeContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            storeContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tc)
        {
            if (tc)
            {
                return await storeContext.Set<TEntity>().ToListAsync(); 
            }
            return await storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
        {
            return await SpecificationEvaluator.GetQuery(storeContext.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await storeContext.Set<TEntity>().FindAsync(key);
        }

        public async Task<TEntity> GetAsync(Specifications<TEntity> specifications)
        {
            return await SpecificationEvaluator.GetQuery(storeContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            storeContext.Set<TEntity>().Update(entity);
        }
    }
}
