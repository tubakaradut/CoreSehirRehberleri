using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Models.Entities
{
    public class Photo : BaseEntity
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAded{ get; set; }
        public bool IsMain{ get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

    }
}
