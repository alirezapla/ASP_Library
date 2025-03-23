using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class updateBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "BookDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "40ef366d-9504-48c2-8304-1fd0da7c28d0", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5792), "", null, "This is about Tech", false, "Tech", "", null },
                    { "80851ca8-e0e8-48d2-8723-7b56f84412fb", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5821), "", null, "Books on science and nature", false, "Science", "", null },
                    { "d6127060-0250-48f8-914a-72d87b87d667", "system", new DateTime(2025, 3, 23, 14, 29, 45, 453, DateTimeKind.Utc).AddTicks(5808), "", null, "Books on Finance ", false, "Finance ", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_PublisherName_IsDeleted",
                table: "Publishers",
                columns: new[] { "PublisherName", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_IsDeleted",
                table: "Categories",
                columns: new[] { "Name", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId_PublisherId_Title_IsDeleted",
                table: "Books",
                columns: new[] { "AuthorId", "PublisherId", "Title", "IsDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Publishers_PublisherName_IsDeleted",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_IsDeleted",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId_PublisherId_Title_IsDeleted",
                table: "Books");

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
                name: "Price",
                table: "BookDetails");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
    }
}
