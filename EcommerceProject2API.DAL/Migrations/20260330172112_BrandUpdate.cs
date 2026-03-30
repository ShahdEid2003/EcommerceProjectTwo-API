using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject2API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BrandUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "BrandsTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "BrandsTranslations");
        }
    }
}
