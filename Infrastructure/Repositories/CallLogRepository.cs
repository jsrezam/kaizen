using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;

namespace Kaizen.Infrastructure.Repositories
{
    public class CallLogRepository : BaseRepository<CallLog>, ICallLogRepository
    {
        public CallLogRepository(KaizenDbContext context) : base(context) { }

    }
}