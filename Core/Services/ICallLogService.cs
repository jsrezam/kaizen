using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public interface ICallLogService
    {
        Task SynchronizeTodayCalls(string userId, IEnumerable<CallLog> callLogs);
    }
}