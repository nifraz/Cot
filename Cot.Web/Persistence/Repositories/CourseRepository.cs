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

        public Task<IPagedList<Course>> GetPageAsync(int pageNumber, int pageSize, string sortField, string sortValue, string filterField, string filterText)
        {
            var query = GetQueryable();

            if (!string.IsNullOrEmpty(filterText))
            {
                query = filterField switch
                {
                    "Code" => query.Where(e => e.Code.Contains(filterText)),
                    "Title" => query.Where(e => e.Title.Contains(filterText)),
                    _ => query.Where(e => e.Code.Contains(filterText) || e.Title.Contains(filterText)),
                };
            }

            if (sortValue == "Descending")
            {
                query = sortField switch
                {
                    "Title" => query.OrderByDescending(e => e.Title),
                    "Level" => query.OrderByDescending(e => e.Level),
                    "AddedDate" => query.OrderByDescending(e => e.AddedDate),
                    "ModifiedDate" => query.OrderByDescending(e => e.ModifiedDate),
                    _ => query.OrderByDescending(e => e.Code),
                };
            }
            else
            {
                query = sortField switch
                {
                    "Title" => query.OrderBy(e => e.Title),
                    "Level" => query.OrderBy(e => e.Level),
                    "AddedDate" => query.OrderBy(e => e.AddedDate),
                    "ModifiedDate" => query.OrderBy(e => e.ModifiedDate),
                    _ => query.OrderBy(e => e.Code),
                };
            }

            return query.ToPagedListAsync(pageNumber, pageSize);
        }

    }
}
