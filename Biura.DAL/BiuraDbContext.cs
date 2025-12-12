using Microsoft.EntityFrameworkCore;
using Biura.Model;

namespace Biura.DAL
{
    public class BiuraDbContext : DbContext
    {
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Registry> Registries { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public BiuraDbContext(DbContextOptions<BiuraDbContext> options) : base(options) 
        { 
            
        }

        internal object Find(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
