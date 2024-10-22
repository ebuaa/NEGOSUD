using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negosud.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupplierOrderCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderDetails_Products_ProductID",
                table: "SupplierOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderDetails_Products_ProductID",
                table: "SupplierOrderDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderDetails_Products_ProductID",
                table: "SupplierOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderDetails_Products_ProductID",
                table: "SupplierOrderDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
