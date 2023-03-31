using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class ShoppingCartTablesAddedToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shippingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.id);
                });




        
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salesPrice = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.productId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCart_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

         
     

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.DropTable(
                name: "CartItems");

       
            migrationBuilder.DropTable(
                name: "ShoppingCart");

        
        }
    }
}
