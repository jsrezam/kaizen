using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;

namespace Kaizen.Infrastructure.Repositories
{
    public class CallLogRepository : BaseRepository<CallLog>, ICallLogRepository
    {
        public CallLogRepository(KaizenDbContext context) : base(context) { }

        // public async Task<bool> SynchronizeTodayCalls(IEnumerable<CallLog> callLogs)
        // {
        //     var userCampaigns = 
        //     // var result = new QueryResult<Campaign>();

        //     // var query = entities
        //     // .Where(c => c.UserId.Equals(userId))
        //     // .OrderByDescending(c => c.Id)
        //     // .AsQueryable();

        //     // query = query.ApplyFiltering(queryObj);

        //     // var columnsMap = new Dictionary<string, Expression<Func<Campaign, object>>>()
        //     // {
        //     //     ["finishDate"] = c => c.FinishDate,
        //     // };
        //     // query = query.ApplyOrdering(queryObj, columnsMap);

        //     // result.TotalItems = await query.CountAsync();
        //     // query = query.ApplyPaging(queryObj);
        //     // result.Items = await query.ToListAsync();

        //     // return result;
        // }

    }
}