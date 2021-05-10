using System;
using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class CampaignQuery : IQueryObject
    {
        public DateTime? FinishDate { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}