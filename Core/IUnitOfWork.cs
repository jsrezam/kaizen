using System;
using System.Threading.Tasks;

namespace Kaizen.Core
{

    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        Task SaveChangesAsync();
    }
}