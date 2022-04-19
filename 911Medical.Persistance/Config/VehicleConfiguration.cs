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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
                .ToTable("Vehicles")
                .HasKey(c => c.Id);

            builder.Property(x => x.RegNumber).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.VehicleType).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(255).IsRequired(false);

            

        }
    }
}
