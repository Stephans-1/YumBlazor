using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YumBlazor.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialtagToProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialTag",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialTag",
                table: "Product");
        }
    }
}
