using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UniTiket.Repositories
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int take = 0);
        public Task<IEnumerable<TEntity>> GetAllAsync<OrderTKey>(int skip = 0, int take = 0, bool orderByDescending = true, Expression<Func<TEntity, OrderTKey>>? orderBy = null);
        public Task<TEntity?> FindByIdAsync(object? id);
        public Task<TEntity> AddAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<TEntity?> DeleteAsync(object id);
        public Task<TEntity> DeleteAsync(TEntity entity);
        public Task<int> SaveChangesAsync();
    }
}
