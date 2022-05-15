using Cot.Data.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Cot.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }

        Task<int> CompleteAsync();
    }
}
