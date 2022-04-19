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
    public class VehicleStateConfiguration : IEntityTypeConfiguration<VehicleState>
    {
        public void Configure(EntityTypeBuilder<VehicleState> builder)
        {
            builder
               //.ToTable("VehicleState")
               .HasKey(x => x.VehicleId);

            builder.HasOne(x => x.Vehicle).WithOne(x => x.State).HasForeignKey<VehicleState>(x=>x.VehicleId);
        }
    }
}
