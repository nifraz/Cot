using Cot.Data.Core.Domain;
using Cot.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Data.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbSet<TEntity> entities;

        protected Repository(CotDbContext context)
        {
            entities = context.Set<TEntity>();
        }

        public Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return entities
                .AnyAsync(predicate);
        }

        public abstract Task<bool> IsExistingAsync(TEntity entity);


        public Task<TEntity> GetAsync(params object[] keyValues)
        {
            return entities
                .FindAsync(keyValues)
                .AsTask();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return entities
                .Where(predicate)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities
                .AsNoTracking()
                .ToListAsync();
        }

        public abstract Task<IEnumerable<TEntity>> GetAllAsync(string sortField, string sortOrder, string searchField, string searchText);

        public Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize)
        {
            return entities
                .AsNoTracking()
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public abstract Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize, string sortField, string sortOrder, string searchField, string searchText);

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }


        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            entities.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }


        protected IQueryable<TEntity> GetQueryable()
        {
            return entities
                .AsNoTracking();
        }

        protected abstract IQueryable<TEntity> GetQueryable(string sortField, string sortValue, string searchField, string searchText);
    }
}
