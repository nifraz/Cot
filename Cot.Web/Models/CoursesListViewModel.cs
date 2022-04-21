using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Web.Models
{
    public class CoursesListViewModel
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
        public string SearchText { get; set; }
        public IPagedList<Course> CoursesList { get; set; }
    }
}
