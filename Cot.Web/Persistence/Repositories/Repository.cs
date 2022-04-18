using Cot.Web.Core.Domain;
using Cot.Web.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cot.Web.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly CotDbContext dbContext;
        private readonly DbSet<TEntity> _entities;

        public Repository(CotDbContext dbContext)
        {
            this.dbContext = dbContext;
            this._entities = dbContext.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return _entities.AsQueryable();
        }

        //public IEnumerable<TEntity> GetAllCached()
        //{
        //    return _entities.Local.ToList();
        //}


        public async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues).AsTask();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }


        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }


        public Task<int> CountAll()
        {
            return _entities.CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.CountAsync(predicate);
        }
    }
}
