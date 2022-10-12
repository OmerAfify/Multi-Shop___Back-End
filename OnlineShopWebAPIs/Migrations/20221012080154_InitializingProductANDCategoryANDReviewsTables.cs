using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopWebAPIs.Migrations
{
    public partial class InitializingProductANDCategoryANDReviewsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    categoryDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salesPrice = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Products", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Tb_Products_Tb_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Tb_Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Reviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reviewDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Reviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Tb_Reviews_Tb_Products_productId",
                        column: x => x.productId,
                        principalTable: "Tb_Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Products_categoryId",
                table: "Tb_Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reviews_productId",
                table: "Tb_Reviews",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_Reviews");

            migrationBuilder.DropTable(
                name: "Tb_Products");

            migrationBuilder.DropTable(
                name: "Tb_Categories");
        }
    }
}
