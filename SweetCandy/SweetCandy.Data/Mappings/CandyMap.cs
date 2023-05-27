using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SweetCandy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCandy.Data.Mappings
{
    internal class CandyMap : IEntityTypeConfiguration<Candy>
    {
        public void Configure(EntityTypeBuilder<Candy> builder)
        {
            builder.ToTable("Candies");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.CategoryId).IsRequired();
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Candies)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Candies_Categories")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
