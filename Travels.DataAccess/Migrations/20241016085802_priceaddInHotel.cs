using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class priceaddInHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Hotel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Hotel");
        }
    }
}
