using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _911Medical.Persistance.Migrations
{
    public partial class Trip_StartAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartAddress_AddressLine1",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddress_AddressLine2",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddress_City",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddress_CountryIso",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddress_ProvinceStateRegionCode",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddress_ZipPostalCode",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartAddress_AddressLine1",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "StartAddress_AddressLine2",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "StartAddress_City",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "StartAddress_CountryIso",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "StartAddress_ProvinceStateRegionCode",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "StartAddress_ZipPostalCode",
                table: "Trip");
        }
    }
}
