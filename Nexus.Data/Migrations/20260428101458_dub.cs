using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Data.Migrations
{
    /// <inheritdoc />
    public partial class dub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Products");
        }
    }
}
