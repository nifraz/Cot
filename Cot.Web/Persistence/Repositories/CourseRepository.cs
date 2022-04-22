using Cot.Web.Core.Domain;
using Cot.Web.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Web.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CotDbContext context) : base(context)
        {

        }

        public Task<IPagedList<Course>> GetAllPagedListAsync(int? pageNumber, int? pageSize, string sortField, string sortValue, string searchText)
        {
            var query = GetQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(e => e.Code.Contains(searchText) || e.Title.Contains(searchText));
            }

            if (sortValue == "Descending")
            {
                query = sortField switch
                {
                    "Title" => query.OrderByDescending(e => e.Title),
                    _ => query.OrderByDescending(e => e.Code),
                };
            }
            else
            {
                query = sortField switch
                {
                    "Title" => query.OrderBy(e => e.Title),
                    _ => query.OrderBy(e => e.Code),
                };
            }

            if (pageNumber == null || pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize == null || pageSize < 1)
            {
                pageSize = 10;
            }

            return query.ToPagedListAsync(pageNumber.Value, pageSize.Value);
        }

    }
}
