using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedTime", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqrt", "UpdatedTime" },
                values: new object[] { 1, "", new DateTime(2023, 1, 31, 20, 14, 52, 866, DateTimeKind.Local).AddTicks(9069), "Temp", "https://www.google.ru/url?sa=i&url=https%3A%2F%2Fwww.booking.com%2Fhotel%2Fgr%2Felysian-villa.ru.html&psig=AOvVaw3Lew8r_MG419GJJcCC45I1&ust=1675271534608000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMC97vem8vwCFQAAAAAdAAAAABAE", "Royal Villa", 5, 200.0, 500, new DateTime(2023, 1, 31, 20, 14, 52, 866, DateTimeKind.Local).AddTicks(9078) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
