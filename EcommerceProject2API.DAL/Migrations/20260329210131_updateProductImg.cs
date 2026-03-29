using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject2API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateProductImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImg",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImg",
                table: "Products");
        }
    }
}
