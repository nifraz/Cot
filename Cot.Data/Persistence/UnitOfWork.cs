using Cot.Data.Core;
using Cot.Data.Core.Repositories;
using Cot.Data.Persistence.Repositories;
using System.Threading.Tasks;

namespace Cot.Data.Persistence
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
