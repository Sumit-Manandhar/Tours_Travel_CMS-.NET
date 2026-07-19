using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vHolidays.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Package_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ClassOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adult = table.Column<int>(type: "int", nullable: false),
                    FreeOfCost = table.Column<int>(type: "int", nullable: false),
                    SingleSupliment = table.Column<bool>(type: "bit", nullable: false),
                    PackageDetailId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PackageGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageGroup_PackageDetails_PackageDetailId",
                        column: x => x.PackageDetailId,
                        principalTable: "PackageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageClassOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    PackageDetailId = table.Column<int>(type: "int", nullable: false),
                    ClassOptionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PackageClassOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageClassOption_ClassOptions_ClassOptionId",
                        column: x => x.ClassOptionId,
                        principalTable: "ClassOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageClassOption_PackageDetails_PackageDetailId",
                        column: x => x.PackageDetailId,
                        principalTable: "PackageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackagePrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PackageDetailId = table.Column<int>(type: "int", nullable: false),
                    PackageClassOptionId = table.Column<int>(type: "int", nullable: false),
                    PackageGroupId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PackagePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagePrice_PackageClassOption_PackageClassOptionId",
                        column: x => x.PackageClassOptionId,
                        principalTable: "PackageClassOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackagePrice_PackageDetails_PackageDetailId",
                        column: x => x.PackageDetailId,
                        principalTable: "PackageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PackagePrice_PackageGroup_PackageGroupId",
                        column: x => x.PackageGroupId,
                        principalTable: "PackageGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageClassOption_ClassOptionId",
                table: "PackageClassOption",
                column: "ClassOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageClassOption_PackageDetailId",
                table: "PackageClassOption",
                column: "PackageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageGroup_PackageDetailId",
                table: "PackageGroup",
                column: "PackageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_PackageClassOptionId",
                table: "PackagePrice",
                column: "PackageClassOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_PackageDetailId",
                table: "PackagePrice",
                column: "PackageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_PackageGroupId",
                table: "PackagePrice",
                column: "PackageGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackagePrice");

            migrationBuilder.DropTable(
                name: "PackageClassOption");

            migrationBuilder.DropTable(
                name: "PackageGroup");

            migrationBuilder.DropTable(
                name: "ClassOptions");
        }
    }
}
