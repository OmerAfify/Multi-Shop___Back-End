using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopWebAPIs.Migrations
{
    public partial class productImagesTableIsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Tb_Products",
                newName: "defaultImageName");

            migrationBuilder.CreateTable(
                name: "Tb_ProductImages",
                columns: table => new
                {
                    productImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_ProductImages", x => x.productImageId);
                    table.ForeignKey(
                        name: "FK_Tb_ProductImages_Tb_Products_productId",
                        column: x => x.productId,
                        principalTable: "Tb_Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_ProductImages_productId",
                table: "Tb_ProductImages",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_ProductImages");

            migrationBuilder.RenameColumn(
                name: "defaultImageName",
                table: "Tb_Products",
                newName: "ImageName");
        }
    }
}
