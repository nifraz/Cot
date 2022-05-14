using Cot.Web.Core.Domain;
using Cot.Web.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Web.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbSet<TEntity> entities;

        protected Repository(CotDbContext context)
        {
            entities = context.Set<TEntity>();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await FindAsync(predicate) != default;
        }

        public abstract Task<bool> IsExistAsync(TEntity entity);


        public async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await entities
                .FindAsync(keyValues)
                .AsTask();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities
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

        public async Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize)
        {
            return await entities
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
