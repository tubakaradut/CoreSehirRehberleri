using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Models.Entities
{
    public class City:BaseEntity
    {
        public City()
        {
            Photos = new List<Photo>();
        }
        
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Photo> Photos { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
