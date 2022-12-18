using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Models.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Cities = new List<City>();
        }
       
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public virtual List<City> Cities { get; set; }
    }
}
