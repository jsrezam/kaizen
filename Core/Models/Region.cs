using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.Models
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<City> cities { get; set; }

        public Region()
        {
            this.cities = new Collection<City>();
        }
    }
}