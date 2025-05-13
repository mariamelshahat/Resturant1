using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurantopia.Migrations
{
    /// <inheritdoc />
    public partial class data2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c8dd1e0-ccac-4cb2-bdb1-1ef887d09c3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93fddd41-5531-40d1-a4ce-2a96cfd07827");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "108d1667-c888-4165-a24f-78669f23be57", "29ec1600-a1db-4bff-a214-0a6d25bb9988", "Customer", "customer" },
                    { "6e7ee39e-0b3e-4194-a250-2a7f99ba2791", "a4cc0557-8086-4dc7-960f-0ad8dd47f1b3", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "108d1667-c888-4165-a24f-78669f23be57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e7ee39e-0b3e-4194-a250-2a7f99ba2791");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c8dd1e0-ccac-4cb2-bdb1-1ef887d09c3a", "6615a9c1-b7db-43c1-8d11-0ceb41cd449a", "Customer", "customer" },
                    { "93fddd41-5531-40d1-a4ce-2a96cfd07827", "6b42f127-f931-4b8d-832b-7a219aa32dbf", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
