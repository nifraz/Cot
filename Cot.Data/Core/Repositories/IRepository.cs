using Cot.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Data.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistingAsync(TEntity entity);

        Task<TEntity> GetAsync(params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(string sortField, string sortOrder, string searchField, string searchText);
        Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize);
        Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize, string sortField, string sortOrder, string searchField, string searchText);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
