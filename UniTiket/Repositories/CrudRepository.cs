using Microsoft.EntityFrameworkCore;
using UniTiKet_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UniTiket.Repositories
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
    {
        public readonly DBContext _context;
        public CrudRepository(DBContext context)
        {
            _context = context;
            _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll<OrderTKey>(int skip, int take, bool orderByDescending,
            Expression<Func<TEntity, OrderTKey>>? orderBy)
            => GetAll(_context.Set<TEntity>(), skip, take, orderByDescending, orderBy);

        public IQueryable<TEntity> GetAll<OrderTKey>(IQueryable<TEntity> source, int skip, int take, bool orderByDescending,
            Expression<Func<TEntity, OrderTKey>>? orderBy, Expression<Func<TEntity, bool>>? whereFunc = null)
        {
            IQueryable<TEntity> queryable = source;

            if (whereFunc != null)
            {
                queryable = queryable.Where(whereFunc);
            }

            if (orderBy != null)
            {
                if (orderByDescending)
                {
                    queryable = queryable.OrderByDescending(orderBy);
                }
                else
                {
                    queryable = queryable.OrderBy(orderBy);
                }
            }

            if (skip != 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (take != 0)
            {
                queryable = queryable.Take(take);
            }

            return queryable;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int take = 0)
        {
            return await GetAll<object>(skip, take, false, null).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<OrderTKey>(int skip = 0, int take = 0, bool orderByDescending = true, Expression<Func<TEntity, OrderTKey>>? orderBy = null)
        {
            return await GetAll(skip, take, orderByDescending, orderBy).ToListAsync();
        }

        public virtual async Task<TEntity?> FindByIdAsync(object? id) => await _context.FindAsync<TEntity>(id);

        public virtual async Task<TEntity> AddAsync(TEntity entity) => (await _context.AddAsync(entity)).Entity;

        public virtual async Task<TEntity> UpdateAsync(TEntity entity) => _context.Update(entity).Entity;

        public virtual async Task<TEntity?> DeleteAsync(object id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null) return null;
            return await DeleteAsync(entity);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity) => _context.Remove(entity).Entity;

        public virtual async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
