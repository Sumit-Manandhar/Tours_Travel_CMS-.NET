using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vHolidays.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactAddress", "ContactEmail", "ContactNumber", "CreatedBy", "CreatedDate", "FacebookLink", "InstagramLink", "IsActive", "IsDeleted", "IsPublished", "Latitude", "Longitude", "MapLinks", "ModifiedBy", "ModifiedDate", "TwitterLink", "YoutubeLink" },
                values: new object[] { 1, "101 Kitchener Road, #03-38 Jalan Besar Plaza, Singapore 208511", "enquiry@vietjetholidays.com", " +65 81619081", "admin@vietjet.com", new DateTime(2024, 11, 21, 12, 51, 27, 838, DateTimeKind.Local).AddTicks(7074), "https://www.facebook.com/", "https://www.instagram.com/", true, false, true, null, null, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.7775084830796!2d103.8554412749657!3d1.3087497986788301!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31da19e1d186e02d%3A0x2242714927df7e83!2sJalan%20Besar%20Plaza!5e0!3m2!1sen!2snp!4v1728475580324!5m2!1sen!2snp", null, null, "https://www.twitter.com/", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
