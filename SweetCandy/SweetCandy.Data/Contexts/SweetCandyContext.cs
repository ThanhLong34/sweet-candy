using Microsoft.EntityFrameworkCore;
using SweetCandy.Core.Entities;
using SweetCandy.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCandy.Data.Contexts
{
    public class SweetCandyContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Candy> Candies { get; set; }

        public SweetCandyContext()
        {

        }

        public SweetCandyContext(DbContextOptions<SweetCandyContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=LAPTOP-QG81K6JN;Database=SweetCandy;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMap).Assembly);
        }
    }
}
