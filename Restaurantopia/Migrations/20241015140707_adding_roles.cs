using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurantopia.Migrations
{
    /// <inheritdoc />
    public partial class adding_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21c347bd-fb35-426c-8c5f-8ec49c86f91f", "9af538e7-65ce-48bb-afa4-5c937f516e42", "Admin", "ADMIN" },
                    { "e70cb147-e4ec-452a-9b8c-6cd5a854baee", "1b23be7c-28e5-482d-8c3b-5ffe086fc46f", "Customer", "customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c347bd-fb35-426c-8c5f-8ec49c86f91f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e70cb147-e4ec-452a-9b8c-6cd5a854baee");
        }
    }
}
