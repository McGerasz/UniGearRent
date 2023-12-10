using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGearRentAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixDescriptionTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descritption",
                table: "Post",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Post",
                newName: "Descritption");
        }
    }
}
