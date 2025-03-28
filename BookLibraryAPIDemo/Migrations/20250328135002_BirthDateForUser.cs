using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class BirthDateForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "470389e2-4c68-4efd-850a-ed3282ae236e",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 13, 50, 2, 470, DateTimeKind.Utc).AddTicks(3081));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8fece538-ab92-4316-9c19-6693514dc283",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 13, 50, 2, 470, DateTimeKind.Utc).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "bd97c0cc-7e68-4a59-935d-8d7d12269cbe",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 13, 50, 2, 470, DateTimeKind.Utc).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "32F8F8D1-5510-45D9-9B39-29A35FDD85EC",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "DateOfBirth", "Email" },
                values: new object[] { "daa294b7-0183-4384-b9f6-da344edc79a4", new DateTime(2025, 3, 28, 13, 50, 2, 477, DateTimeKind.Utc).AddTicks(6110), new DateTime(1989, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@h.c" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_IsDeleted",
                table: "Users",
                columns: new[] { "Email", "IsDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "470389e2-4c68-4efd-850a-ed3282ae236e",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 0, 46, 24, 6, DateTimeKind.Utc).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8fece538-ab92-4316-9c19-6693514dc283",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 0, 46, 24, 6, DateTimeKind.Utc).AddTicks(6355));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "bd97c0cc-7e68-4a59-935d-8d7d12269cbe",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 0, 46, 24, 6, DateTimeKind.Utc).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "32F8F8D1-5510-45D9-9B39-29A35FDD85EC",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "Email" },
                values: new object[] { "3807f8fd-a453-4e53-ab39-34d347550f44", new DateTime(2025, 3, 28, 0, 46, 24, 14, DateTimeKind.Utc).AddTicks(1316), null });
        }
    }
}
