// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _911Medical.Persistance;

#nullable disable

namespace _911Medical.Persistance.Migrations
{
    [DbContext(typeof(MedicalDbContext))]
    [Migration("20220412025751_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("_911Medical.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("_911Medical.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RegNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("VehicleType")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("_911Medical.Domain.Entities.Patient", b =>
                {
                    b.OwnsOne("_911Medical.Domain.Entities.Address", "HomeAddress", b1 =>
                        {
                            b1.Property<int>("PatientId")
                                .HasColumnType("int");

                            b1.Property<string>("AddressLine1")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("AddressLine2")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("CountryIso")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("ProvinceStateRegionCode")
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.Property<string>("ZipPostalCode")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("HomeAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
