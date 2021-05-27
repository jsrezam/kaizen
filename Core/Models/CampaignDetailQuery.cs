using System;
using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class CampaignDetailQuery : IQueryObject
    {
        public Customer Customer { get; set; }
        public int? TotalCallsNumber { get; set; }
        public string LastCallDuration { get; set; }
        public DateTime LastCallDate { get; set; }
        public string LastValidCallDuration { get; set; }
        public DateTime LastValidCallDate { get; set; }
        public string State { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}