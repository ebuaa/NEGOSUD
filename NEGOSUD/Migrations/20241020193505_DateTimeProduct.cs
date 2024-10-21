using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEGOSUD.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Products");
        }
    }
}
