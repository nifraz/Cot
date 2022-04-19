using Cot.Web.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }

        Task<int> CompleteAsync();
    }
}
