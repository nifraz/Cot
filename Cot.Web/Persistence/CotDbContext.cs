using Cot.Web.Core.Domain;
using Cot.Web.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Persistence
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
