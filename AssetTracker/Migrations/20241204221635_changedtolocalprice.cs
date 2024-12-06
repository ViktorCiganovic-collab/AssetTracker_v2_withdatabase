using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracker.Migrations
{
    /// <inheritdoc />
    public partial class changedtolocalprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EuroPrice",
                table: "Products",
                newName: "LocalPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalPrice",
                table: "Products",
                newName: "EuroPrice");
        }
    }
}
