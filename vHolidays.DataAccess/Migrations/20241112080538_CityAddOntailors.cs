using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vHolidays.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CityAddOntailors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "TailorMadeTrip",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TailorMadeTrip_CityId",
                table: "TailorMadeTrip",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TailorMadeTrip_City_CityId",
                table: "TailorMadeTrip",
                column: "CityId",
                principalSchema: "Region",
                principalTable: "City",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TailorMadeTrip_City_CityId",
                table: "TailorMadeTrip");

            migrationBuilder.DropIndex(
                name: "IX_TailorMadeTrip_CityId",
                table: "TailorMadeTrip");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "TailorMadeTrip");
        }
    }
}
