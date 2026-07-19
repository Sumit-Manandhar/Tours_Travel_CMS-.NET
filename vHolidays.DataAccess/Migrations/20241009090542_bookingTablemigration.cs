using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vHolidays.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookingTablemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    Vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    NonVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    SpecialRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencySalutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyRelation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedFlight = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageBookings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageBookings");
        }
    }
}
