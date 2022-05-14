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

        public override async Task<bool> IsExistAsync(Course entity)
        {
            return await FindAsync(e => e.Id == entity.Id || e.Code == entity.Code || e.Title == entity.Title) != default;
        }

        public override async Task<IEnumerable<Course>> GetAllAsync(string sortField, string sortOrder, string searchField, string searchText)
        {
            return await GetQueryable(sortField, sortOrder, searchField, searchText)
                .ToListAsync();
        }

        public override async Task<IPagedList<Course>> GetPageAsync(int pageNumber, int pageSize, string sortField, string sortOrder, string searchField, string searchText)
        {
            return await GetQueryable(sortField, sortOrder, searchField, searchText)
                .ToPagedListAsync(pageNumber, pageSize);
        }

        protected override IQueryable<Course> GetQueryable(string sortField, string sortOrder, string searchField, string searchText)
        {
            var query = GetQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = searchField switch
                {
                    "Code" => query.Where(e => e.Code.Contains(searchText)),
                    "Title" => query.Where(e => e.Title.Contains(searchText)),
                    _ => query.Where(e => e.Code.Contains(searchText) || e.Title.Contains(searchText)),
                };
            }

            if (sortOrder == "Descending")
            {
                query = sortField switch
                {
                    "Title" => query.OrderByDescending(e => e.Title),
                    "Level" => query.OrderByDescending(e => e.Level),
                    "Type" => query.OrderByDescending(e => e.Type),
                    "AddedDateTime" => query.OrderByDescending(e => e.AddedDateTime),
                    "ModifiedDateTime" => query.OrderByDescending(e => e.ModifiedDateTime),
                    _ => query.OrderByDescending(e => e.Code),
                };
            }
            else
            {
                query = sortField switch
                {
                    "Title" => query.OrderBy(e => e.Title),
                    "Level" => query.OrderBy(e => e.Level),
                    "Type" => query.OrderBy(e => e.Type),
                    "AddedDateTime" => query.OrderBy(e => e.AddedDateTime),
                    "ModifiedDateTime" => query.OrderBy(e => e.ModifiedDateTime),
                    _ => query.OrderBy(e => e.Code),
                };
            }
            return query;
        }
    }
}
