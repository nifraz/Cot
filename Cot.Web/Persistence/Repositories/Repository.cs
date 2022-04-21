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
        private readonly CotDbContext context;
        private readonly DbSet<TEntity> entitySet;

        public Repository(CotDbContext context)
        {
            this.context = context;
            this.entitySet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return entitySet
                .AsQueryable();
        }

        //public IEnumerable<TEntity> GetAllCached()
        //{
        //    return _entities.Local.ToList();
        //}


        public async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await entitySet
                .FindAsync(keyValues)
                .AsTask();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entitySet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetAllPagedListAsync(int? pageNumber, int? pageSize, string sortField, string sortValue, string searchText)
        {
            return await entitySet
                .AsNoTracking()
                .ToPagedListAsync(pageNumber ?? 1, pageSize ?? 10);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entitySet
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entitySet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }


        public void Add(TEntity entity)
        {
            entitySet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            entitySet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            entitySet.Update(entity);
            //entitySet.Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            entitySet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            entitySet.RemoveRange(entities);
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
