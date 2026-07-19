using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FkHotelPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Country_CountryId",
                table: "Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_PackageHotel_HotelId",
                table: "PackageHotel",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageHotel_PackageId",
                table: "PackageHotel",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Country_CountryId",
                table: "Hotel",
                column: "CountryId",
                principalSchema: "Region",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageHotel_Hotel_HotelId",
                table: "PackageHotel",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageHotel_PackageDetails_PackageId",
                table: "PackageHotel",
                column: "PackageId",
                principalTable: "PackageDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Country_CountryId",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageHotel_Hotel_HotelId",
                table: "PackageHotel");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageHotel_PackageDetails_PackageId",
                table: "PackageHotel");

            migrationBuilder.DropIndex(
                name: "IX_PackageHotel_HotelId",
                table: "PackageHotel");

            migrationBuilder.DropIndex(
                name: "IX_PackageHotel_PackageId",
                table: "PackageHotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Country_CountryId",
                table: "Hotel",
                column: "CountryId",
                principalSchema: "Region",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
