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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients")
                .HasKey(c => c.Id);

            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.OwnsOne(x => x.HomeAddress, ownsBuilder =>
            {
                ownsBuilder.Property(x => x.AddressLine1).HasMaxLength(100).IsRequired(true);
                ownsBuilder.Property(x => x.AddressLine2).HasMaxLength(100).IsRequired(false);
                ownsBuilder.Property(x => x.City).HasMaxLength(100).IsRequired(true);
                ownsBuilder.Property(x => x.ZipPostalCode).HasMaxLength(30).IsRequired(true);
                ownsBuilder.Property(x => x.ProvinceStateRegionCode).HasMaxLength(30).IsRequired(false);
                ownsBuilder.Property(x => x.CountryIso).HasMaxLength(10).IsRequired(true);
            });
        }
    }
}
