using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurantopia.Migrations
{
    /// <inheritdoc />
    public partial class data1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c347bd-fb35-426c-8c5f-8ec49c86f91f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e70cb147-e4ec-452a-9b8c-6cd5a854baee");

            migrationBuilder.DropColumn(
                name: "dbimage",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ItemImage",
                table: "Items",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c8dd1e0-ccac-4cb2-bdb1-1ef887d09c3a", "6615a9c1-b7db-43c1-8d11-0ceb41cd449a", "Customer", "customer" },
                    { "93fddd41-5531-40d1-a4ce-2a96cfd07827", "6b42f127-f931-4b8d-832b-7a219aa32dbf", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c8dd1e0-ccac-4cb2-bdb1-1ef887d09c3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93fddd41-5531-40d1-a4ce-2a96cfd07827");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "ItemImage",
                table: "Items",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "dbimage",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21c347bd-fb35-426c-8c5f-8ec49c86f91f", "9af538e7-65ce-48bb-afa4-5c937f516e42", "Admin", "ADMIN" },
                    { "e70cb147-e4ec-452a-9b8c-6cd5a854baee", "1b23be7c-28e5-482d-8c3b-5ffe086fc46f", "Customer", "customer" }
                });
        }
    }
}
