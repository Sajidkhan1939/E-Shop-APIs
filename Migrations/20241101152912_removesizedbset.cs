using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtImplementation.Migrations
{
    /// <inheritdoc />
    public partial class removesizedbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SizeCategories_SizeCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SizeCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SizeCategoryId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SizeCategories",
                columns: table => new
                {
                    SizeCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeCategories", x => x.SizeCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeCategoryId",
                table: "Products",
                column: "SizeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SizeCategories_SizeCategoryId",
                table: "Products",
                column: "SizeCategoryId",
                principalTable: "SizeCategories",
                principalColumn: "SizeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
