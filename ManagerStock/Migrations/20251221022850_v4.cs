using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerStock.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                schema: "public",
                table: "products");

            migrationBuilder.RenameTable(
                name: "products",
                schema: "public",
                newName: "product",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                schema: "public",
                table: "product",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                schema: "public",
                table: "product");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "public",
                newName: "products",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                schema: "public",
                table: "products",
                column: "id");
        }
    }
}
