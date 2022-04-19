using _911Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Persistance.Config
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder
                .ToTable("Trip")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.Vehicle);
            builder.HasOne(x => x.Patient).WithMany().IsRequired();
            builder.OwnsOne(x => x.StartAddress);
        }
    }
}
