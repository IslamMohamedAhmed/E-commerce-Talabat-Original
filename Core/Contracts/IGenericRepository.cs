using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool tc = false);
        public Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);

        public Task<TEntity> GetAsync(TKey key);
        public Task<TEntity> GetAsync(Specifications<TEntity> specifications);

        public Task AddAsync(TEntity entity);
        
        public void Update(TEntity entity);

        public void Delete(TEntity entity);
        
    }
}
