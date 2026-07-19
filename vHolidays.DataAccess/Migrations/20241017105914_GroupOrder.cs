using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vHolidays.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GroupOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PackageGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PackageGroup");
        }
    }
}
