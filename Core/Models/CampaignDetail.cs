using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Core.Models
{
    public class CampaignDetail : BaseEntity
    {
        public int CampaignId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CallsNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CallDuration { get; set; }
        public string Status { get; set; }
    }
}