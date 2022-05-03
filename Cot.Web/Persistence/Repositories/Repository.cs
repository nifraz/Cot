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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        //private readonly CotDbContext context;
        private readonly DbSet<TEntity> entities;

        public Repository(CotDbContext context)
        {
            //this.context = context;
            entities = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return entities
                .AsNoTracking();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await FindAsync(predicate) != default;
        }

        public async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await entities
                .FindAsync(keyValues)
                .AsTask();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetAllPagedListAsync(int? pageNumber, int? pageSize)
        {
            return await entities
                .AsNoTracking()
                .ToPagedListAsync(pageNumber ?? 1, pageSize ?? 10);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

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
            //entitySet.Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }


        //public Task<int> CountAll()
        //{
        //    return entitySet.CountAsync();
        //}

        //public Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return entitySet.CountAsync(predicate);
        //}
    }
}
