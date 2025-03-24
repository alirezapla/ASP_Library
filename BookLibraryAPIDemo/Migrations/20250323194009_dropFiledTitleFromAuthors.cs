using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class dropFiledTitleFromAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "40ef366d-9504-48c2-8304-1fd0da7c28d0");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "80851ca8-e0e8-48d2-8723-7b56f84412fb");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "d6127060-0250-48f8-914a-72d87b87d667");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Authors");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "700e44f2-a2ad-4139-8ac1-a8171729488d", "system", new DateTime(2025, 3, 23, 19, 40, 9, 254, DateTimeKind.Utc).AddTicks(6834), "", null, "This is about Tech", false, "Tech", "", null },
                    { "8b39aabe-6c51-4528-a200-c33d0f8d1f4e", "system", new DateTime(2025, 3, 23, 19, 40, 9, 254, DateTimeKind.Utc).AddTicks(6851), "", null, "Books on Finance ", false, "Finance ", "", null },
                    { "ebe07541-97db-4f0b-95ce-adebfae77667", "system", new DateTime(2025, 3, 23, 19, 40, 9, 254, DateTimeKind.Utc).AddTicks(6865), "", null, "Books on science and nature", false, "Science", "", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "700e44f2-a2ad-4139-8ac1-a8171729488d");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8b39aabe-6c51-4528-a200-c33d0f8d1f4e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ebe07541-97db-4f0b-95ce-adebfae77667");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "40ef366d-9504-48c2-8304-1fd0da7c28d0", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5792), "", null, "This is about Tech", false, "Tech", "", null },
                    { "80851ca8-e0e8-48d2-8723-7b56f84412fb", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5821), "", null, "Books on science and nature", false, "Science", "", null },
                    { "d6127060-0250-48f8-914a-72d87b87d667", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5808), "", null, "Books on Finance ", false, "Finance ", "", null }
                });
        }
    }
}
