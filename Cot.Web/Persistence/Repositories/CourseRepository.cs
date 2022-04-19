using Cot.Web.Core.Domain;
using Cot.Web.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CotDbContext context) : base(context)
        {

        }
    }
}
