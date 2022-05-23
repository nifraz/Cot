using Cot.Data.Core.Domain;
using Cot.Data.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Data.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CotDbContext context) : base(context)
        {

        }

        public override async Task<bool> IsExistingAsync(Course entity)
        {
            return await IsExistingAsync(e => e.Id == entity.Id || e.Code == entity.Code || e.Title == entity.Title);
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
                switch (searchField)
                {
                    case "Code": query = query.Where(e => e.Code.Contains(searchText)); break;
                    case "Title": query = query.Where(e => e.Title.Contains(searchText)); break;
                    default: query = query.Where(e => e.Code.Contains(searchText) || e.Title.Contains(searchText)); break;
                };
            }

            if (sortOrder == "Descending")
            {
                switch (sortField)
                {
                    case "Title": query = query.OrderByDescending(e => e.Title); break;
                    case "Level": query = query.OrderByDescending(e => e.Level); break;
                    case "Type": query = query.OrderByDescending(e => e.Type); break;
                    case "AddedDateTime": query = query.OrderByDescending(e => e.AddedDateTime); break;
                    case "ModifiedDateTime": query = query.OrderByDescending(e => e.ModifiedDateTime); break;
                    default: query = query.OrderByDescending(e => e.Code); break;
                };
            }
            else
            {
                switch (sortField)
                {
                    case "Title": query = query.OrderBy(e => e.Title); break;
                    case "Level": query = query.OrderBy(e => e.Level); break;
                    case "Type": query = query.OrderBy(e => e.Type); break;
                    case "AddedDateTime": query = query.OrderBy(e => e.AddedDateTime); break;
                    case "ModifiedDateTime": query = query.OrderBy(e => e.ModifiedDateTime); break;
                    default: query = query.OrderBy(e => e.Code); break;
                };
            }
            return query;
        }
    }
}
