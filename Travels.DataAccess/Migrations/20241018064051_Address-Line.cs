using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddressLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Hotel");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Hotel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Hotel");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Hotel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
