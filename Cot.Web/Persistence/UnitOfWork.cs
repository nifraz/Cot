using Cot.Web.Core;
using Cot.Web.Core.Repositories;
using Cot.Web.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CotDbContext context;

        public UnitOfWork(CotDbContext context)
        {
            this.context = context;

            Courses = new CourseRepository(this.context);
        }

        public ICourseRepository Courses { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
