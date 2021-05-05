using System;

namespace Kaizen.Core.Models
{
    public class CallLog : BaseEntity
    {
        public string CallName { get; set; }
        public string CallNumber { get; set; }
        public string CallNumberFormatted { get; set; }
        public string CallDuration { get; set; }
        public string CallDurationFormat { get; set; }
        public long CallDateTick { get; set; }
        public DateTime CallDate { get; set; }
        public string CallType { get; set; }
        public string CallTitle { get; set; }
        public string CallDescription { get; set; }
        public int CallTimes { get; set; }
    }
}