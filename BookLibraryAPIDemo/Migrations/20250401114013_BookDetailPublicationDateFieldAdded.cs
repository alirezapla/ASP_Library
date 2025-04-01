using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class BookDetailPublicationDateFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "BookDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "470389e2-4c68-4efd-850a-ed3282ae236e",
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 11, 40, 13, 522, DateTimeKind.Utc).AddTicks(6862));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8fece538-ab92-4316-9c19-6693514dc283",
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 11, 40, 13, 522, DateTimeKind.Utc).AddTicks(6872));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "bd97c0cc-7e68-4a59-935d-8d7d12269cbe",
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 11, 40, 13, 522, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "32F8F8D1-5510-45D9-9B39-29A35FDD85EC",
                columns: new[] { "ConcurrencyStamp", "CreatedDate" },
                values: new object[] { "cc294785-2149-4780-b7ea-6de13e5ab73b", new DateTime(2025, 4, 1, 11, 40, 13, 530, DateTimeKind.Utc).AddTicks(4489) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "BookDetails");

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
                columns: new[] { "ConcurrencyStamp", "CreatedDate" },
                values: new object[] { "daa294b7-0183-4384-b9f6-da344edc79a4", new DateTime(2025, 3, 28, 13, 50, 2, 477, DateTimeKind.Utc).AddTicks(6110) });
        }
    }
}
