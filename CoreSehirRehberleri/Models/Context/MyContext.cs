
using CoreSehirRehberleri.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreSehirRehberleri.Models.Context
{
    public class MyContext:DbContext
    {

        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
