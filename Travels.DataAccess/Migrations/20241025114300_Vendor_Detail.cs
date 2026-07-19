using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Vendor_Detail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentralEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationEmail1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationEmail2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_SupplierDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierDetail_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Region",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierDetail_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Region",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SupplierDetail_State_StateId",
                        column: x => x.StateId,
                        principalSchema: "Region",
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierDetailId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SupplierContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContact_SupplierDetail_SupplierDetailId",
                        column: x => x.SupplierDetailId,
                        principalTable: "SupplierDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierDestination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    SupplierDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDestination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierDestination_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Region",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierDestination_SupplierDetail_SupplierDetailId",
                        column: x => x.SupplierDetailId,
                        principalTable: "SupplierDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SupplierProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierProducts_SupplierDetail_SupplierDetailId",
                        column: x => x.SupplierDetailId,
                        principalTable: "SupplierDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierDestinationCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    SupplierDestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDestinationCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierDestinationCity_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Region",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierDestinationCity_SupplierDestination_SupplierDestinationId",
                        column: x => x.SupplierDestinationId,
                        principalTable: "SupplierDestination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContact_SupplierDetailId",
                table: "SupplierContact",
                column: "SupplierDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDestination_CountryId",
                table: "SupplierDestination",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDestination_SupplierDetailId",
                table: "SupplierDestination",
                column: "SupplierDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDestinationCity_CityId",
                table: "SupplierDestinationCity",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDestinationCity_SupplierDestinationId",
                table: "SupplierDestinationCity",
                column: "SupplierDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDetail_CityId",
                table: "SupplierDetail",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDetail_CountryId",
                table: "SupplierDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDetail_StateId",
                table: "SupplierDetail",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProducts_SupplierDetailId",
                table: "SupplierProducts",
                column: "SupplierDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierContact");

            migrationBuilder.DropTable(
                name: "SupplierDestinationCity");

            migrationBuilder.DropTable(
                name: "SupplierProducts");

            migrationBuilder.DropTable(
                name: "SupplierDestination");

            migrationBuilder.DropTable(
                name: "SupplierDetail");
        }
    }
}
