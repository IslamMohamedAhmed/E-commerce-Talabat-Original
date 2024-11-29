using Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;
        private readonly ConcurrentDictionary<string,object> repository;
        public UnitOfWork(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            repository = new();
        }
        public  IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return (IGenericRepository<TEntity, TKey>) repository.GetOrAdd(typeof(TEntity).Name, (_) =>
            
                 new GenericRepository<TEntity, TKey>(storeContext)
            );
        }

        public async Task<int> SaveChangesAsync()
        {
            return await storeContext.SaveChangesAsync();
        }
    }
}
