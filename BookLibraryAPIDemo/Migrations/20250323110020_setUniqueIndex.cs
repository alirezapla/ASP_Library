using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class setUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "4a6e3e51-bb20-46c0-be5a-5acd66a16503");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "7814eceb-e25b-4760-aff3-92818913f082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ed0445a0-2c75-4474-a2ef-a3e458507428");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "043a11f6-0dd7-40ae-bb80-c85a88507798", "system", new DateTime(2025, 3, 23, 11, 0, 19, 655, DateTimeKind.Utc).AddTicks(7623), "", null, "Books on science and nature", false, "Science", "", null },
                    { "05e00d7e-c166-4c45-a58b-7093b4f0f6d0", "system", new DateTime(2025, 3, 23, 11, 0, 19, 655, DateTimeKind.Utc).AddTicks(7593), "", null, "This is about Tech", false, "Tech", "", null },
                    { "a882ce77-621f-45a1-9160-73b82e7fe31b", "system", new DateTime(2025, 3, 23, 11, 0, 19, 655, DateTimeKind.Utc).AddTicks(7610), "", null, "Books on Finance ", false, "Finance ", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_PublisherName",
                table: "Publishers",
                column: "PublisherName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId_PublisherId_Title",
                table: "Books",
                columns: new[] { "AuthorId", "PublisherId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Publishers_PublisherName",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId_PublisherId_Title",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "043a11f6-0dd7-40ae-bb80-c85a88507798");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "05e00d7e-c166-4c45-a58b-7093b4f0f6d0");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "a882ce77-621f-45a1-9160-73b82e7fe31b");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "4a6e3e51-bb20-46c0-be5a-5acd66a16503", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2164), "", null, "This is about Tech", false, "Tech", "", null },
                    { "7814eceb-e25b-4760-aff3-92818913f082", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2181), "", null, "Books on Finance ", false, "Finance ", "", null },
                    { "ed0445a0-2c75-4474-a2ef-a3e458507428", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2192), "", null, "Books on science and nature", false, "Science", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }
    }
}
