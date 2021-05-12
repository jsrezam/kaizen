using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Region> Regions { get; set; }

        public Country()
        {
            this.Regions = new Collection<Region>();
        }
    }
}