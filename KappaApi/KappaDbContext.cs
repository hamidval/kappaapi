using KappaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KappaApi
{
    public class KappaDbContext : DbContext
    {
        public KappaDbContext(DbContextOptions<KappaDbContext> options) : base(options)
        {

        }


        public DbSet<Parent> Parents { get; set; }
    }

}
