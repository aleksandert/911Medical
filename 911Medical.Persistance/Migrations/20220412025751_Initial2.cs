using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _911Medical.Persistance.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_AddressLine1",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_AddressLine2",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_City",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_CountryIso",
                table: "Patients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_ProvinceStateRegionCode",
                table: "Patients",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_ZipPostalCode",
                table: "Patients",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeAddress_AddressLine1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_AddressLine2",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_CountryIso",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_ProvinceStateRegionCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_ZipPostalCode",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryIso = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProvinceStateRegionCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ZipPostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
