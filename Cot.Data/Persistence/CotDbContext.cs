using Cot.Data.Core.Domain;
using Cot.Data.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cot.Data.Persistence
{
    public class CotDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public CotDbContext(DbContextOptions<CotDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CourseConfiguration().Configure(modelBuilder.Entity<Course>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
