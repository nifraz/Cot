using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Web.Core.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<bool> IsExistAsync(TEntity entity);
        Task<IPagedList<TEntity>> GetPageAsync(int pageNumber, int pageSize, string sortField, string sortValue, string searchField, string searchText);
    }
}
